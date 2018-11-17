using Game.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
                builder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.ConnectionString);
        }
    }
}
