using Slutprojekt.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt.Models.ViewModels
{
    public class HomeVM
    {
		public string User { get; set; }

		public Match[] recentMatches { get; set; }

		public News[] RecentNews { get; set; }

		public News[] OldNews { get; set; }

	}
}
