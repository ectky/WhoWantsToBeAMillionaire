using Game.Data.Services.Contracts;
using Game.Models;
using System.Linq;

namespace Game.Data.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly GameDbContext context;

        public PlayersService()
        {
            context = new GameDbContext();
        }

        public Player CurrentPlayer { get; private set; }

        public void LogPlyerWithUsername(string username)
        {
            if (context.Players.Any(p => p.Username == username))
            {
                this.CurrentPlayer = context.Players.First(p => p.Username == username);
                return;
            }

            var player = new Player()
            {
                Username = username
            };

            this.context.Players.Add(player);
            this.context.SaveChanges();
        }
    }
}
