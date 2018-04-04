using Slutprojekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt
{
    public class GameMemory
    {

        /// <summary>
        ///  Gets or sets the value indicating whether the game is over.
        /// </summary>
        public bool IsOver { get; private set; }

        /// <summary>
        /// Gets or sets the value indicating whether the game is draw.
        /// </summary>
        public bool IsDraw { get; private set; }

        /// <summary>
        /// Gets or sets Player 1 of the game
        /// </summary>
        public PlayerMemory Player1 { get; set; }

        /// <summary>
        /// Gets or sets Player 2 of the game
        /// </summary>
        public PlayerMemory Player2 { get; set; }

        public int[] FieldList { get; set; }
        /// <summary>
        /// For internal housekeeping, To keep track of value in each of the box in the grid.
        /// </summary>
        private readonly int[] field0 = new int[8];
        private readonly int[] field = new int[16];

         
        /// <summary>
        /// The number of moves left. We start the game with 9 moves remaining in a 3x3 grid.
        /// </summary>
        private int movesLeft = 150;
        private int pairsLeft;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public GameMemory()
        {
            //// Initialize the game
            for (var i = 0; i < 8; i++)
            {
                field0[i]+=i;
            }
            Array.Copy(field0, 0, field, 0,8);
            Array.Copy(field0, 0, field, 8,8);
            Random rnd = new Random();
            FieldList = field.OrderBy(c => rnd.Next()).ToArray(); ;
            
        }
        /// <summary>
        /// Place the player number at a given position for a player
        /// </summary>
        /// <param name="player="symbol"">The player number would be 0 or 1</param>
        /// <param name="position">The position where player number would be placed, should be between 0 and 8, both inclusive</param>
        /// <returns>Boolean true if game is over and we have a winner.</returns>
        public bool  Play(int player, int[] positions)
        {
            if (this.IsOver)
            {
                return false;
            }

            //// Place the player number at the given position
            ////Are this a match?
            int matchOrNot =this.AreThisPairAMatch(player, positions);


            //// Check if we have a winner. If this returns true, 
            //// game would be over and would have a winner, else game would continue.
            return this.CheckWinner(matchOrNot);
        }

        /// <summary>
        /// Checks for the winner by inspecting different combination of winning combinations
        /// Notice that each position is initialized with -1, meaning no player has placed his number there.
        /// </summary>
        /// <returns>Boolean true if we have a winner.</returns>
        private bool CheckWinner(int matchOrNot)
        {
            if (matchOrNot==1)
            {
                pairsLeft++;
            }

            if (pairsLeft>=8)
            {
                this.IsOver = true; //// Game is over
                return true;   //// We have a winner
            }

            return false; //// Game can go on, we still don't have a winner.
        }

        /// <summary>
        /// Places the player number at the given position for the player if the position is marked as -1, i.e., not taken
        /// </summary>
        /// <param name="player">The player number, i.e, 0 or 1</param>
        /// <param name="position">The position to place the player number, should be between 0 and 8, both inclusive.</param>
        public int AreThisPairAMatch(int player, int[] positions)
        {
            this.movesLeft -= 1;

            if (this.movesLeft <= 0)
            {
                //// We are out of moves, so game is over and is draw
                this.IsOver = true;
                this.IsDraw = true;
            }
            //ev ett fel här? på array
            if (FieldList[positions[0]] == FieldList[positions[1]])
            {
                //you have a match
                return 1;
            }
            else
            {
                //not a match
                return 0;
            }
        }
    }
}





