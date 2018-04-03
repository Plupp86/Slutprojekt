using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Slutprojekt.Stats
{
	public partial class StatsDBContext : DbContext
	{
		public StatsDBContext(DbContextOptions<StatsDBContext> options) : base(options)
		{
		}
	}
}
