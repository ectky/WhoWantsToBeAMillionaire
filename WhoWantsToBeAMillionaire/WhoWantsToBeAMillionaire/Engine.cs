using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game.App.Contracts;
using Game.App.Entities;
using Game.App.Factories;

namespace Game.App
{
    public class Engine
    {
        private static string Source = DirectoryProvider.CurrentDirectory();
        private string questionsTxt = Source + "questions.txt";

        private IReader reader;
        private IGameFactory gameFactory;
        private ILevelFactory levelFactory;
        private IQuestionFactory questionFactory;
        private int countOfLevels;
        private int countOfQuestionsInLevel;

        public Engine()
        {
            this.gameFactory = new GameFactory();
            this.levelFactory = new LevelFactory();
            this.questionFactory = new QuestionFactory();
            this.reader = new Reader();

            SetCountsAndPrizes();

            this.Game = LoadGame();
        }

        public string[] Prizes { get; private set; }

        public int GetCountOfLevels()
        {
            return this.countOfLevels;
        }

        private void SetCountsAndPrizes()
        {
            string file = questionsTxt;
            int[] counts = File.ReadAllLines(file)[0].Split().Select(int.Parse).ToArray();
            this.Prizes = File.ReadAllLines(file)[1].Split().ToArray();

            this.countOfLevels = counts[0];
            this.countOfQuestionsInLevel = counts[1];
        }

        public IGameLevels Game { get; private set; }

        private IGameLevels LoadGame()
        {
            IGameLevels game = new GameLevels();
            for (int i = 0; i < this.countOfLevels; i++)
            {
                ILevel level = LoadLevel();
                game.AddLevel(level);
            }

            return game;
        }

        private ILevel LoadLevel()
        {
            ILevel level = new Level();

            for (int i = 0; i < this.countOfQuestionsInLevel; i++)
            {
                string questionText = reader.ReadLine();
                string answerA = reader.ReadLine();
                string answerB = reader.ReadLine();
                string answerC = reader.ReadLine();
                string answerD = reader.ReadLine();
                string rightAnswer = answerA;
                string closeAnswer = answerD;
                IQuestion question = this.questionFactory.Create(questionText, rightAnswer,
                    closeAnswer, answerA, answerB, answerC, answerD);

                level.AddQuestion(question);
            }

            return level;
        }
    }
}