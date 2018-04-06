using Slutprojekt.Models.Entities;
using Slutprojekt.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt.Stats
{
	public class StatsRepository
	{
		StatsDBContext context;


		public StatsRepository(StatsDBContext context)
		{
			this.context = context;
		}

		public void MoveExistingUsers(string[] users)
		{


			foreach (var item in users)
			{
				if (!context.User.Any(u => u.UserName == item))
				{
				context.User.Add(new User { UserName = item });
				}
			}
			context.SaveChanges();
		}

		public void ReportMatch(Match match)
		{
			var player1 = context.User
				.FirstOrDefault(u => u.UserName == match.Player1);
			var player2 = context.User
				.FirstOrDefault(u => u.UserName == match.Player2);


			switch (match.Game)
			{
				case "MathGame":
					player1.MathMatches++;
					player2.MathMatches++;

					if (player1.UserName == match.Winner)
					{
						player1.MathWon++;
					}
					else
					{
						player2.MathWon++;
					}
					break;
				case "Tic-Tac-Toe":
					player1.TicMatches++;
					player2.TicMatches++;

					if (player1.UserName == match.Winner)
					{
						player1.TicWon++;
					}
					else if (player2.UserName == match.Winner)
					{
						player2.TicWon++;
					}
					break;
				case "Memory":
					player1.MemoryMatches++;
					player2.MemoryMatches++;

					if (player1.UserName == match.Winner)
					{
						player1.MemoryWon++;
					}
					else if (player2.UserName == match.Winner)
					{
						player2.MemoryWon++;
					}
					break;
				default:
					break;
			}

			context.Match.Add(match);
			context.SaveChanges();
		}

		public int GetId(string userName)
		{
			var user = context.User
				.FirstOrDefault(u => u.UserName == userName);
			return user.Id;

		}

		public Match[] GetRecentMatches()
		{
			return context.Match
				.OrderByDescending(m => m.Id)
				.Take(5)
				.ToArray();
		}

		public User[] GetMathUsers()
		{
			return context.User
				.Where(u => u.MathMatches > 0)
				.OrderByDescending(u => u.MathWon)
				.ToArray();
		}

		public User[] GetTicUsers()
		{
			return context.User
				.Where(u => u.TicMatches > 0)
				.OrderByDescending(u => u.TicWon)
				.ToArray();
		}

		public User[] GetMemoryUsers()
		{
			return context.User
				.Where(u => u.MemoryMatches > 0)
				.OrderByDescending(u => u.MemoryWon)
				.ToArray();
		}

		internal void ReportTheNews(AddNewsVM model)
		{
			var news = new News()
			{
				Author = model.UserName,
				Story = model.TheNews,
				Date = DateTime.Now
			};
			context.News.Add(news);
			context.SaveChanges();
		}

		internal News[] GetTheNews()
		{
			return context.News
				.OrderByDescending(n => n.Date)
				.Take(5)
				.ToArray();
		}

		internal News[] GetOldNews()
		{
			if (context.News.Count() < 6)
			{
				return null;
			}

			return context.News
				.OrderByDescending(n => n.Date)
				.Skip(5)
				.Take(5)
				.ToArray();
		}

		internal void CreateUser(User user)
		{
			context.User.Add(user);
			context.SaveChanges();

		}
	}
}
