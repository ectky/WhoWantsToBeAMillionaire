using Game.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext()
        {

        }

        public GameDbContext(DbContextOptions options)
        {
            
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game.Models.Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
                builder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.ConnectionString);
        }
    }
}
