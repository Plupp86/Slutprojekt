using System;
using System.Collections.Generic;

namespace Slutprojekt.Stats
{
    public partial class User
    {
        public User()
        {
            MatchPlayer1Navigation = new HashSet<Match>();
            MatchPlayer2Navigation = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public int TicMatches { get; set; }
        public int TicWon { get; set; }
        public int MathMatches { get; set; }
        public int MathWon { get; set; }
        public int MemoryMatches { get; set; }
        public int MemoryWon { get; set; }

        public ICollection<Match> MatchPlayer1Navigation { get; set; }
        public ICollection<Match> MatchPlayer2Navigation { get; set; }
    }
}
