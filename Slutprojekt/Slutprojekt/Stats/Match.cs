using System;
using System.Collections.Generic;

namespace Slutprojekt.Stats
{
    public partial class Match
    {
        public int Id { get; set; }
        public int Player1 { get; set; }
        public int? Player2 { get; set; }
        public int? Winner { get; set; }
        public bool? Draw { get; set; }
        public string Game { get; set; }

        public User Player1Navigation { get; set; }
        public User Player2Navigation { get; set; }
    }
}
