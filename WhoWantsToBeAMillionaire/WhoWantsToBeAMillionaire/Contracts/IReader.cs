namespace Game.App
{
    public interface IReader
    {
        void Open();

        string ReadLine();
    }
}