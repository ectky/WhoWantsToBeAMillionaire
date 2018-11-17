using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.App.Entities;

namespace Game.App.Contracts
{
    public interface IMediaNavigator
    {
        IReadOnlyDictionary<Sound, string> SoundDictionary { get; }

        string GetSound(Sound correct);
    }
}
