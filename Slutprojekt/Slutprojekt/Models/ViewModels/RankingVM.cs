using Slutprojekt.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt.Models.ViewModels
{
    public class RankingVM
    {
		public User[] MathUsers { get; set; }
		public User[] TicUsers { get; set; }
		public User[] MemoryUsers { get; set; }
	}
}
