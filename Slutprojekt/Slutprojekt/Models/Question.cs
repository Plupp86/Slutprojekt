using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt.Models
{
    public class Question
    {
		Random rand = new Random();

		public Question()
		{
			Number1 = rand.Next(20);
			Number2 = rand.Next(20);

			CorrectAnswer = Number1 * Number2;
			int ans = 0;
			int answer;
			Answers = new int[9];

			while (ans < 9)
			{
				answer = rand.Next(400);

				if (answer != CorrectAnswer)
				{
					Answers[ans++] = answer;
				}
			}
			Answers[rand.Next(9)] = CorrectAnswer;
		}

		public int Number1 { get; set; }
		public int Number2 { get; set; }

		public int CorrectAnswer { get; set; }

		public int[] Answers { get; set; }


	}
}
