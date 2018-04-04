using System;
using System.Collections.Generic;

namespace Slutprojekt.Stats
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int TicMatches { get; set; }
        public int TicWon { get; set; }
        public int MathMatches { get; set; }
        public int MathWon { get; set; }
        public int MemoryMatches { get; set; }
        public int MemoryWon { get; set; }
    }
}
