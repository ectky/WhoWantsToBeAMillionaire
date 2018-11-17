using System.IO;
using System.Runtime.InteropServices;
using Game.App.Entities;

namespace Game.App
{
    public class Reader : IReader
    {
        private string FILE = DirectoryProvider.CurrentDirectory() + "questions.txt";

        private int currentLine;
        private string[] lines;

        public Reader()
        {
            this.currentLine = 1;

            this.Open();
        }

        private void ChangeLine()
        {
            this.currentLine++;
        }

        public string ReadLine()
        {
            string result = lines[currentLine];
            this.ChangeLine();
            return result;
        }

        public void Open()
        {
            this.lines = File.ReadAllLines(FILE);
        }
    }
}