using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Media;
using System.Threading;
using Game.App.Contracts;
using Game.App.Entities;
using WhoWantsToBeAMillionaire;
using Game.Data.Services.Contracts;

namespace Game.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string Source = DirectoryProvider.CurrentDirectory();

        private string StartQuestionSound = Source + "letsplay.wav";
        private string FinalAnswerSound = Source + "final answer.wav";
        private string WrongAnswerSound = Source + "wrong answer.wav";
        private string RightAnswerSound = Source + "correct answer.wav";
        private string FiftyFiftySound = Source + "fifty.wav";
        private string AskTheAudienceSound = Source + "askTheAudience.wav";

        private string FiftyPicture = Source + "fifty.png";
        private string FiftyUsedPicture = Source + "fiftyUsed.png";
        private string AskTheAudiencePicture = Source + "people.png";
        private string AskTheAudienceUsedPicture = Source + "fiftyUsed.png";
        private string PrziesNonePicture = Source + "rewards.png";
        private string PrziesOnePicture = Source + "rewards.png";
        private string PrziesTwoPicture = Source + "rewards.png";
        private string PrziesThreePicture = Source + "rewards.png";
        private string PrziesFourPicture = Source + "rewards.png";
        private string PrziesFivePicture = Source + "rewards.png";

        private Engine engine;
        private int levelCount;
        private ILevel currentLevel;
        private Button[] answers;

        private readonly IPlayersService playersService;
        private readonly IGamesService gamesService;

        public MainWindow(IPlayersService playersService, IGamesService gamesService)
        {
            InitializeComponent();

            this.playersService = playersService;
            this.gamesService = gamesService;

            this.answers = new[] { AnswerA, AnswerB, AnswerC, AnswerD };
            this.engine = new Engine();
            this.levelCount = 0;
            LoadLevel();
        }

        private void LoadLevel()
        {
            if (levelCount >= this.engine.Game.Size - 1)
            {
                EndGame end = new EndGame(playersService, gamesService);
                end.ShowDialog();
                this.Close();
            }
            var regForm = new RegisterPlayer(playersService, gamesService);

            regForm.ShowDialog();

            ResetJokers();

            this.currentLevel = engine.Game.GetCurrentLevel(this.levelCount);
            
            LoadQuestion();

            this.levelCount++;
        }

        private void LoadQuestion()
        {
            currentLevel.ChangeQuestion();

            if (this.currentLevel.IsEndOfQuestions)
            {
                YouWin youWin = new YouWin();
                youWin.ShowDialog();
                LoadLevel();
            }

            SoundPlayer sound = new SoundPlayer(StartQuestionSound);
            sound.Play();

            ChangePrize();

            QuestionLabel.Text = currentLevel.Current.QuestionText;

            GenerateAnswers(currentLevel.Current);
        }

        private void ChangePrize()
        {
            string[] prizes =
            {
                PrziesOnePicture,
                PrziesTwoPicture,
                PrziesThreePicture,
                PrziesFourPicture,
                PrziesFivePicture
            };
            string path = prizes[this.currentLevel.CurrentCount];

            Fifty.BeginInit();
            Fifty.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            Fifty.EndInit();
        }

        private void GenerateAnswers(IQuestion currentQuestion)
        {
            string[] letters = {"A: ", "B: ", "C: ", "D: "};

            currentQuestion.Shuffle();

            for (int i = 0; i < currentQuestion.Answers.Length; i++)
            {
                this.answers[i].Content = letters[i] + currentQuestion.Answers[i];
                this.answers[i].Background = Brushes.Black;
            }
        }

        private void ResetJokers()
        {
            string path = FiftyPicture;

            Fifty.BeginInit();
            Fifty.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            Fifty.EndInit();

            path = AskTheAudiencePicture;

            People.BeginInit();
            People.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            People.EndInit();

            path = PrziesNonePicture;

            Prizes.BeginInit();
            Prizes.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            Prizes.EndInit();
        }

        private void fiftyHelper_MouseDown(object sender, MouseEventArgs e)
        {
            string path = FiftyUsedPicture;

            SoundPlayer sound = new SoundPlayer(FiftyFiftySound);
            sound.Play();

            Fifty.BeginInit();
            Fifty.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            Fifty.EndInit();

            EliminateTwoAnswers();
        }

        private void EliminateTwoAnswers()
        {
            Thread.Sleep(500);
            foreach (var answer in this.answers)
            {
                string buttonContent = answer.Content.ToString().Substring(3);
                
                if (!this.currentLevel.Current.IsRightAnswer(buttonContent) &&
                    !this.currentLevel.Current.IsCloseAnswer(buttonContent))
                {
                    answer.Content = string.Empty;
                }
            }
        }

        private void peopleHelper_MouseDown(object sender, MouseEventArgs e)
        {
            string path = AskTheAudienceUsedPicture;

            SoundPlayer sound = new SoundPlayer(AskTheAudienceSound);
            sound.PlayLooping();

            People.BeginInit();
            People.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            People.EndInit();
        }

        private void AnswerA_Click(object sender, RoutedEventArgs e)
        {
            AnswerA.Background = Brushes.Orange;
            SoundPlayer sound1 = new SoundPlayer(FinalAnswerSound);
            sound1.Play();
            Thread.Sleep(8000);

            string answerA = AnswerA.Content.ToString().Substring(3);
            if (this.currentLevel.Current.IsRightAnswer(answerA))
            {
                AnswerA.Background = Brushes.Green;

                SoundPlayer sound = new SoundPlayer(RightAnswerSound);
                sound.Play();

                //adds a point to the current players score
                this.gamesService.AddPointToPlayersScore();

                Thread.Sleep(10000);

                LoadQuestion();
            }
            else
            {
                AnswerA.Background = Brushes.Red;

                SoundPlayer sound = new SoundPlayer(WrongAnswerSound);
                sound.Play();

                Thread.Sleep(5000);

                LoadLevel();
            }
        }

        private void AnswerB_Click(object sender, RoutedEventArgs e)
        {
            AnswerB.Background = Brushes.Orange;
            SoundPlayer sound1 = new SoundPlayer(FinalAnswerSound);
            sound1.Play();
            Thread.Sleep(8000);

            string answerB = AnswerB.Content.ToString().Substring(3);
            if (this.currentLevel.Current.IsRightAnswer(answerB))
            {
                AnswerB.Background = Brushes.Green;
                SoundPlayer sound = new SoundPlayer(RightAnswerSound);
                sound.Play();

                //adds a point to the current players score
                this.gamesService.AddPointToPlayersScore();

                Thread.Sleep(10000);

                LoadQuestion();
            }
            else
            {
                AnswerB.Background = Brushes.Red;

                SoundPlayer sound = new SoundPlayer(WrongAnswerSound);
                sound.Play();

                Thread.Sleep(10000);

                LoadLevel();
            }
        }

        private void AnswerC_Click(object sender, RoutedEventArgs e)
        {
            AnswerC.Background = Brushes.Orange;
            SoundPlayer sound1 = new SoundPlayer(FinalAnswerSound);
            sound1.Play();
            Thread.Sleep(8000);

            string answerC = AnswerC.Content.ToString().Substring(3);
            if (this.currentLevel.Current.IsRightAnswer(answerC))
            {
                AnswerC.Background = Brushes.Green;
                SoundPlayer sound = new SoundPlayer(RightAnswerSound);
                sound.Play();

                //adds a point to the current players score
                this.gamesService.AddPointToPlayersScore();

                Thread.Sleep(10000);

                LoadQuestion();
            }
            else
            {
                AnswerC.Background = Brushes.Red;

                SoundPlayer sound = new SoundPlayer(WrongAnswerSound);
                sound.Play();

                Thread.Sleep(5000);

                LoadLevel();
            }
        }

        private void AnswerD_Click(object sender, RoutedEventArgs e)
        {
            AnswerD.Background = Brushes.Orange;
            SoundPlayer sound1 = new SoundPlayer(FinalAnswerSound);
            sound1.Play();
            Thread.Sleep(8000);

            string answerC = AnswerD.Content.ToString().Substring(3);
            if (this.currentLevel.Current.IsRightAnswer(answerC))
            {
                AnswerD.Background = Brushes.Green;
                SoundPlayer sound = new SoundPlayer(RightAnswerSound);
                sound.Play();

                //adds a point to the current players score
                this.gamesService.AddPointToPlayersScore();

                Thread.Sleep(10000);
                LoadQuestion();

            }
            else
            {
                AnswerD.Background = Brushes.Red;

                SoundPlayer sound = new SoundPlayer(WrongAnswerSound);
                sound.Play();

                Thread.Sleep(5000);

                LoadLevel();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
        }
    }
}
