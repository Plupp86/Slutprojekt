using Microsoft.AspNetCore.SignalR;
using Slutprojekt.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt
{
    public partial class MathHub : Hub
    {
		private static ConcurrentBag<MathGame> mathGames = new ConcurrentBag<MathGame>();

		private static ConcurrentBag<Player> mathPlayers = new ConcurrentBag<Player>();

		public void MakeAGuess(int pos)
		{
			//// Lets find a game from our list of games where one of the player has the same connection Id as the current connection has.

			//var game = mathGames?.FirstOrDefault(x => x.Player1.ConnectionId == Context.ConnectionId || x.Player2.ConnectionId == Context.ConnectionId);

			var game = mathGames
				.FirstOrDefault(g => g.Player1.ConnectionId == Context.ConnectionId || g.Player2.ConnectionId == Context.ConnectionId);

			if (game == null || game.IsOver)
			{
				//// No such game exist!
				return;
			}

			if (game.CheckAnswer(Context.ConnectionId, pos))
			{
				Clients.Client(game.Player1.ConnectionId).InvokeAsync(Constants.CorrectAnswer, pos);
				Clients.Client(game.Player2.ConnectionId).InvokeAsync(Constants.CorrectAnswer, pos);
				game.Round++;

				if (game.CheckForWinner())
				{
					var winner = game.Player1Score > game.Player2Score ? game.Player1.Name : game.Player2.Name;

					Clients.Client(game.Player1.ConnectionId).InvokeAsync("winner", winner);
					Clients.Client(game.Player2.ConnectionId).InvokeAsync("winner", winner);
				}
				else
				{
					Clients.Client(game.Player1.ConnectionId).InvokeAsync("nextQuestion", game);
					Clients.Client(game.Player2.ConnectionId).InvokeAsync("nextQuestion", game);
				}

			}
			else
			{
				Clients.Client(game.Player1.ConnectionId).InvokeAsync(Constants.WrongAnswer, pos);
				Clients.Client(game.Player2.ConnectionId).InvokeAsync(Constants.WrongAnswer, pos);
			}
		}

		internal void StartGame(MathGame game)
		{

			//mathGames.Add(game);
			Clients.User(game.Player1.ConnectionId).InvokeAsync("startMathGame", game);
			Clients.User(game.Player2.ConnectionId).InvokeAsync("startMathGame", game);

		}

		public void RegisterPlayer(string userName)
		{
			var player = mathPlayers?.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

			if (player == null)
			{
				player = new Player { ConnectionId = Context.ConnectionId, Name = userName, IsPlaying = false, IsSearchingOpponent = false, RegisterTime = DateTime.UtcNow};
				if (!mathPlayers.Any(j => j.Name == userName))
				{
					mathPlayers.Add(player);
				}
			}
			else
			{
				player.IsPlaying = false;
				player.IsSearchingOpponent = false;
			}

			OnRegisterationComplete(Context.ConnectionId);
		}

		public void OnRegisterationComplete(string connectionId)
		{
			//// Notify this connection id that the registration is complete.
			Clients.Client(connectionId).InvokeAsync(Constants.RegistrationComplete);
		}

		public void FindOpponent()
		{
			//// First fetch the player from our players collection having current connection id
			var player = mathPlayers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
			if (player == null)
			{
				//// Since player would be registered before making this call,
				//// we should not reach here. If we are here, something somewhere in the flow above is broken.
				return;
			}

			//// Set that player is seraching for opponent.
			player.IsSearchingOpponent = true;

			//// We will follow a queue, so find a player who registered earlier as opponent. 
			//// This would only be the case if more than 2 players are looking for opponent.
			var opponent = mathPlayers.Where(x => x.ConnectionId != Context.ConnectionId && x.IsSearchingOpponent && !x.IsPlaying).OrderBy(x => x.RegisterTime).FirstOrDefault();
			if (opponent == null)
			{
				//// Could not find any opponent, invoke opponentNotFound method in the client.
				Clients.Client(Context.ConnectionId).InvokeAsync(Constants.OpponentNotFound, mathPlayers);
				return;
			}

			//// Set both players as playing.
			player.IsPlaying = true;
			player.IsSearchingOpponent = false; //// Make him unsearchable for opponent search

			opponent.IsPlaying = true;
			opponent.IsSearchingOpponent = false;

			//// Set each other as opponents.
			//player.Opponent = opponent;
			//opponent.Opponent = player;

			//// Notify both players that they can play the game by invoking opponentFound method for both the players.
			//// Also pass the opponent name and opoonet image, so that they can visualize it.
			//// Here we are directly using connection id, but group is a good candidate and use here.
			//Clients.Client(Context.ConnectionId).InvokeAsync(Constants.OpponentFound, opponent.Name, opponent.Image);
			//Clients.Client(opponent.ConnectionId).InvokeAsync(Constants.OpponentFound, player.Name, player.Image);

			var newGame = new MathGame(player, opponent);

			mathGames.Add(newGame);

			Clients.All.InvokeAsync("hello");
			Clients.All.InvokeAsync("startMathGame", newGame);
			//Clients.User(newGame.Player2.ConnectionId).InvokeAsync("startMathGame", newGame);
			//// Create a new game with these 2 player and add it to games collection.

			//StartGame(newGame);

		}

		private ConcurrentBag<T> Remove<T>(ConcurrentBag<T> players, T playerWithoutGame)
		{
			return new ConcurrentBag<T>(players?.Except(new[] { playerWithoutGame }));
		}

		public Task OnOpponentDisconnected(string connectionId, string playerName)
		{
			//// Notify this connection id that the given player name has disconnected.
			return Clients.Client(connectionId).InvokeAsync(Constants.OpponentDisconnected, playerName);
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			//// Lets find a game if any of which player 1 / player 2 is disconnected.
			var game = mathGames?.FirstOrDefault(j => j.Player1.ConnectionId == Context.ConnectionId || j.Player2.ConnectionId == Context.ConnectionId);
			if (game == null)
			{
				//// We are here so it means we have no game whose players were disconnected. 
				//// But there may be a scenario that a player is not playing but disconnected and is still in our list.
				//// So, lets remove that player from our list.
				var playerWithoutGame = mathPlayers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
				if (playerWithoutGame != null)
				{
					//// Remove this player from our player list.
					mathPlayers = Remove<Player>(mathPlayers, playerWithoutGame);
				}

				return null;
			}

			//// We have a game in which the player got disconnected, so lets remove that game from our list.
			if (game != null)
			{
				mathGames = Remove<MathGame>(mathGames, game);
			}

			//// Though we have removed the game from our list, we still need to notify the opponent that he has a walkover.
			//// If the current connection Id matches the player 1 connection Id, its him who disconnected else its player 2
			var player = game.Player1.ConnectionId == Context.ConnectionId ? game.Player1 : game.Player2;

			if (player == null)
			{
				return null;
			}

			//// Remove this player as he is disconnected and was in the game.
			mathPlayers = Remove<Player>(mathPlayers, player);

			//// Check if there was an opponent of the player. If yes, tell him, he won/ got a walk over.
			if (player.Opponent != null)
			{
				return OnOpponentDisconnected(player.Opponent.ConnectionId, player.Name);
			}

			return base.OnDisconnectedAsync(exception);
		}

		public override Task OnConnectedAsync()
		{
			return base.OnConnectedAsync();
		}

	}
}
