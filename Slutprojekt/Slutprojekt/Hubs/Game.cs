using Microsoft.AspNetCore.SignalR;
using Slutprojekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt.Hubs
{
    public class Game : Hub
    {
		private static object _syncRoot = new object();
		private static int _gamesPlayed = 0;

		private static readonly List<Client> clients = new List<Client>();

		private static readonly List<TicTacToe> games = new List<TicTacToe>();

		private static readonly Random random = new Random();

		public Task OnDisconnectedAsync()
		{
			var game = games.FirstOrDefault(x => x.Player1.ConnectionId == Context.ConnectionId || x.Player2.ConnectionId == Context.ConnectionId);
			if (game == null)
			{
				var clientWithoutGame = clients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
				if (clientWithoutGame != null)
				{
					clients.Remove(clientWithoutGame);
					SendStatsUpdate();
				}
				return null;
			}

			if (game != null)
			{
				games.Remove(game);
			}

			var client = game.Player1.ConnectionId == Context.ConnectionId ? game.Player1 : game.Player2;

			if (client == null)
			{
				return null;
			}

			clients.Remove(client);
			if (client.Opponent != null)
			{
				SendStatsUpdate();
				return Clients.Client(client.Opponent.ConnectionId).opponentDisconnected(client.Name);
			}

			return null;
		}

		public Task OnConnected()
		{
			return SendStatsUpdate();
		}

		public Task SendStatsUpdate()
		{
			return Clients.All.refreshAmountOfPlayers(new { totalGamesPlayed = _gamesPlayed, amountOfGames = games.Count, amountOfClients = clients.Count });
		}

		public void RegisterClient(string data)
		{
			lock (_syncRoot)
			{
				var client = clients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
				if (client == null)
				{
					client = new Client { ConnectionId = Context.ConnectionId, Name = data };
					clients.Add(client);
				}
				client.IsPlaying = false;
			}
			SendStatsUpdate();
			Clients.Client(Context.ConnectionId).registerComplete();
		}

		public void Play(int position)
		{
			var game = games.FirstOrDefault(x => x.Player1.ConnectionId == Context.ConnectionId || x.Player2.ConnectionId == Context.ConnectionId);

			if (game == null || game.IsGameOver) return;

			int marker = 0;

			if (game.Player2.ConnectionId == Context.ConnectionId)
			{
				marker = 1;
			}

			var player = marker == 0 ? game.Player1 : game.Player2;

			if (player.WaitingForMove) return;

			Clients.Client(game.Player1.ConenctionId).addMarkerPlacement(new GameInformation { OpponentName = player.Name, MarkerPosition = position });
			Clients.Client(game.Player2.ConenctionId).addMarkerPlacement(new GameInformation { OpponentName = player.Name, MarkerPosition = position });

			if (game.Play(marker, position))
			{
				games.Remove(game);
				_gamesPlayed += 1;
				Clients.Client(game.Player1.ConnectionId).gameOver(player.Name);
				Clients.Client(game.Player2.ConnectionId).gameOver(player.Name);
			}

			if (game.IsGameOver && game.IsDraw)
			{
				games.Remove(game);
				_gamesPlayed += 1;
				Clients.Client(game.Player1.ConenctionId).gameOver("It's a Draw!");
				Clients.Client(game.Player2.ConenctionId).gameOver("It's a Draw!");
			}

			if (!game.IsGameOver)
			{
				player.WaitingForMove = !player.WaitingForMove;
				player.Opponent.WaitingForMove = !player.Opponent.WaitingForMove;

				Clients.Client(player.Opponent.ConnectionId).waitingForMarkerPlacement(player.Name);
			}

			SendStatsUpdate();
		}

		public void FindOpponent()
		{
			var player = clients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
			if (player == null) return;

			player.LookingForOpponent = true;

			var opponent = clients.Where(x => x.ConnectionId != Context.ConnectionId && x.LookingForOpponent && !x.IsPlaying).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
			if (opponent == null)
			{
				Clients.Client(Context.ConnectionId).noOpponents();
				return;
			}

			player.IsPlaying = true;
			player.LookingForOpponent = false;
			opponent.IsPlaying = true;
			opponent.LookingForOpponent = false;

			player.Opponent = opponent;
			opponent.Opponent = player;

			Clients.Client(Context.ConnectionId).foundOpponent(opponent.Name);
			Clients.Client(opponent.ConnectionId).foundOpponent(player.Name);

			if (random.Next(0,5000) % 2 == 0)
			{
				player.WaitinForMove = false;
				opponent.WaitingForMove = true;

				Clients.Client(player.ConnectionId).waitingForMarkerPlacement(opponent.Name);
				Clients.Client(opponent.ConnectionId).waitingForOpponent(opponent.Name);
			}
			else
			{
				player.WaitingForMove = true;
				opponent.WaitingForMove = false;

				Clients.Client(opponent.ConnectionId).waitinfForMarkerPlacement(opponent.Name);
				Clients.Client(player.ConnectionId).waitingForOpponent(opponent.Name);
			}

			lock (_syncRoot)
			{
				games.Add(new TicTacToe { Player1 = player, Player2 = opponent });
			}

			SendStatsUpdate();
		}
	}
}
