using Game.Data.Services.Contracts;
using Game.Models;
using System;

namespace Game.Data.Services
{
    public class GamesService : IGamesService
    {
        private readonly GameDbContext context;
        private readonly IPlayersService playersService;

        public GamesService(IPlayersService playersService)
        {
            context = new GameDbContext();
            this.playersService = playersService;
        }

        public Models.Game CurrentGame { get; private set; }

        public void AddPointToPlayersScore()
        {
            this.context.Attach(CurrentGame);

            this.CurrentGame.Score++;

            this.context.SaveChanges();
        }

        public void LogGame()
        {
            var game = new Models.Game()
            {
                Score = 0,
                PlayedOn = DateTime.Now,
                PlayerId = playersService.CurrentPlayer.Id
            };

            context.Games.Add(game);

            this.CurrentGame = game;

            context.SaveChanges();
        }
    }
}
