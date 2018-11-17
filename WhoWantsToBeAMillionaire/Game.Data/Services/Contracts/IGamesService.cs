namespace Game.Data.Services.Contracts
{
    public interface IGamesService
    {
        Game.Models.Game CurrentGame { get; }

        void LogGame();
        void AddPointToPlayersScore();
    }
}
