using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Slutprojekt.Models.Entities
{
    public partial class SlutprojektDBContext : DbContext
    {
        public SlutprojektDBContext(DbContextOptions<SlutprojektDBContext> options) : base(options)
        {
        }
    }
}
