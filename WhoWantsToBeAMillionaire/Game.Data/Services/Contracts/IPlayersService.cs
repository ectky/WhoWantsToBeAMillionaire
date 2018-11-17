using Game.Models;

namespace Game.Data.Services.Contracts
{
    public interface IPlayersService
    {
        Player CurrentPlayer { get; }

        void LogPlyerWithUsername(string username);
    }
}
