using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.App.Contracts;

namespace Game.App.Entities
{
    public class MediaNavigator : IMediaNavigator
    {
        private Dictionary<Sound, string> soundDictionary;
        private const string sourcePath = "pack://application:,,,/Resources/";

        public MediaNavigator()
        {
            soundDictionary = new Dictionary<Sound, string>()
            {
                {Sound.Wrong, sourcePath + "wrong answer.mp3"},
                {Sound.Correct, sourcePath + "correct answer.mp3"},
                {Sound.Final, sourcePath + "final answer.mp3"},
                {Sound.PeopleHelp, sourcePath + "phone a friend.mp3"},
                {Sound.StartQestion, sourcePath + "letsplay.wav"},
                {Sound.DuringQuestion, sourcePath + "DuringQuestion.mp3"},
                {Sound.MainTheme, sourcePath + "main theme.mp3"}
            };
        }

        public IReadOnlyDictionary<Sound, string> SoundDictionary
            => (IReadOnlyDictionary<Sound, string>) soundDictionary;

        public string GetSound(Sound text)
        {
            return soundDictionary[text];
        }
    }
}
