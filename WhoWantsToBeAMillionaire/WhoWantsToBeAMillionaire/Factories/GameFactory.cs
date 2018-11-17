using Game.App.Contracts;
using Game.App.Entities;

namespace Game.App
{
    public class GameFactory : IGameFactory
    {
        public IGameLevels Create(params ILevel[] levels)
        {
            IGameLevels game = new GameLevels(levels);

            return game;
        }
    }
}