using System;
using System.Collections.Generic;

namespace Slutprojekt.Stats
{
    public partial class Match
    {
        public int Id { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Winner { get; set; }
        public bool Draw { get; set; }
        public string Game { get; set; }
    }
}
