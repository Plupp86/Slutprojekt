using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt
{
	/// <summary>
	/// The class for keeping all the constant string. Here we are storing all the client side methods that are invoked from SignalR hub.
	/// </summary>
	public class Constants
	{
		/// <summary>
		///  Stores the registrationComplete method of the client.
		/// </summary>
		public const string RegistrationComplete = "registrationComplete";
		public const string RegistrationCompleteMemory = "registrationCompleteMemory";

        /// <summary>
        ///  Stores the waitingForOpponent method of the client.
        /// </summary>
        public const string WaitingForOpponent = "waitingForOpponent";
        public const string WaitingForOpponentMemory = "waitingForOpponentMemory";

        /// <summary>
        ///  Stores the opponentFound method of the client.
        /// </summary>
        public const string OpponentFound = "opponentFound";
        public const string OpponentFoundMemory = "opponentFoundMemory";

        /// <summary>
        ///  Stores the opponentNotFound method of the client.
        /// </summary>
        public const string OpponentNotFound = "opponentNotFound";
        public const string OpponentNotFoundMemory = "opponentNotFoundMemory";

        /// <summary>
        ///  Stores the opponentDisconnected method of the client.
        /// </summary>
        public const string OpponentDisconnected = "opponentDisconnected";
        public const string OpponentDisconnectedMemory = "opponentDisconnectedMemory";

        /// <summary>
        ///  Stores the waitingForMove method of the client.
        /// </summary>
        public const string WaitingForMove = "waitingForMove";
        public const string WaitingForMoveMemory = "waitingForMoveMemory";

        /// <summary>
        ///  Stores the moveMade method of the client.
        /// </summary>
        public const string MoveMade = "moveMade";
        public const string MoveMadeMemory = "moveMadeMemory";

        /// <summary>
        ///  Stores the gameOver method of the client.
        /// </summary>
        public const string GameOver = "gameOver";
        public const string GameOverMemory = "gameOverMemory";

        public const string NoMAtch = "noMatch";
        public const string GetPositionsArrayMemory = "getPositionsArrayMemory";
        public const string WaitingforMoveCheck = "waitingforMoveCheck";

        





    }
}
