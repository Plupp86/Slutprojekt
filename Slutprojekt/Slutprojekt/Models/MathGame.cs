using Slutprojekt.Models;

namespace Slutprojekt.Hubs
{
	internal class MathGame
	{
		public Player Player1 { get; set; }
		public Player Player2 { get; set; }

		public int Player1Score { get; set; }
		public int Player2Score { get; set; }

		public int Round { get; set; }

		public Question[] Questions { get; set; }

		public bool IsOver { get; set; }

		public MathGame(Player p1, Player p2)
		{
			Player1 = p1;
			Player2 = p2;
			Round = 0;
			Player1Score = 0;
			Player2Score = 0;
			IsOver = false;

			Questions = new Question[9];

			for (int i = 0; i < 9; i++)
			{
				Questions[i] = new Question();
			}
		}

		public bool CheckForWinner()
		{
			if (Player1Score == 5 || Player2Score == 5)
			{
				IsOver = true;
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool CheckAnswer(string playerId, int pos)
		{
			if (Questions[Round].Answers[pos] == Questions[Round].CorrectAnswer)
			{
				if (Player1.ConnectionId == playerId)
				{
					Player1Score++;

				}
				else
				{
					Player2Score++;
				}
				//Round++;
				return true;
			}

			return false;

		}
	}
}