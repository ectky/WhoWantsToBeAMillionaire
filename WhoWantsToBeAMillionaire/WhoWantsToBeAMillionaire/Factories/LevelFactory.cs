using System.Collections.Generic;
using Game.App.Contracts;
using Game.App.Entities;

namespace Game.App
{
    public class LevelFactory : ILevelFactory
    {
        public ILevel Create(params IQuestion[] questions)
        {
            ILevel level = new Level(questions);

            return level;
        }
    }
}