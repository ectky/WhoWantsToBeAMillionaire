using System;
using Game.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoWantsToBeAMillionaire.Services.Contracts;

namespace WhoWantsToBeAMillionaire.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly GameDbContext context;

        public PlayersService()
        {
            this.context = new GameDbContext();
        }

        //public Player CurrentPlayer { get; set; }

        //public void LogPlyerWithUsername(string username)
        //{
        //    if (context.Players)
        //    {

        //    }
        //}
    }
}
