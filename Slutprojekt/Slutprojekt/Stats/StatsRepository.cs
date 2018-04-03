using Slutprojekt.Models.Entities;
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
				context.User.Add(new User { UserName = item });
			}
			context.SaveChanges();
		}

		public void ReportMatch(Match match)
		{
			var player1 = context.User
				.FirstOrDefault(u => u.Id == match.Player1);
			var player2 = context.User
				.FirstOrDefault(u => u.Id == match.Player2);

			player1.MathMatches++;
			player2.MathMatches++;

			if (player1.Id == match.Winner)
			{
				player1.MathWon++;
			}
			else
			{
				player2.MathWon++;
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

	}
}
