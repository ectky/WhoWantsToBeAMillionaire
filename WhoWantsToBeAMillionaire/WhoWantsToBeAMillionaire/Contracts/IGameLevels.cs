using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.App.Contracts
{
    public interface IGameLevels : IEnumerable<ILevel>
    {
        ILevel GetCurrentLevel(int levelCount);

        IList<ILevel> Levels { get; }

        void AddLevel(ILevel level);

        int Size { get; }
    }
}
