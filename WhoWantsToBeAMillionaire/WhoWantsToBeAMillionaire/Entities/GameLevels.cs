using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.App.Contracts;

namespace Game.App.Entities
{
    public class GameLevels : IGameLevels
    {
        public GameLevels()
        {
            this.Levels = new List<ILevel>();
        }

        public GameLevels(params ILevel[] levels) : this()
        {
            this.Levels = levels;
        }

        public IList<ILevel> Levels { get; }

        public int Size => Levels?.Count ?? 0;

        public void AddLevel(ILevel level)
        {
            this.Levels.Add(level);
        }

        public ILevel GetCurrentLevel(int levelCount)
        {
            return this.Levels[levelCount];
        }

        public IEnumerator<ILevel> GetEnumerator()
        {
            foreach (var level in this.Levels)
            {
                yield return level;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
