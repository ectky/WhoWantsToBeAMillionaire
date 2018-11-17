using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.App.Entities;

namespace Game.App.Contracts
{
    public interface ILevelFactory
    {
        ILevel Create(params IQuestion[] questions);
    }
}
