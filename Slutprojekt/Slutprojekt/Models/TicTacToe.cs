using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt.Models
{
	public class TicTacToe
	{
		public bool IsGameOver { get; private set; }
		public bool IsDraw { get; private set; }
		public Client Player1 { get; set; }
		public Client Player2 { get; set; }


		private readonly int[] field = new int[9];
		private int movesLeft = 9;

		public TicTacToe()
		{

			for (int i = 0; i < field.Length; i++)
			{
				field[i] = -1;
			}
		}

		public bool Play(int player, int position)
		{
			if (IsGameOver)
				return false;
			

			PlaceMarker(player, position);
			return CheckWinner();			
		}

		private bool CheckWinner()
		{
			for (int i = 0; i < 3; i++)
			{



			}
		}


			



	}
}
