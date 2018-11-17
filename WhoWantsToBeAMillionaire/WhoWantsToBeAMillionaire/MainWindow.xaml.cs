using Game.App.Contracts;
using Game.App.Entities;
using Game.Data.Services.Contracts;
using System;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WhoWantsToBeAMillionaire;

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

        private readonly Engine engine;
        private readonly Button[] answers;
        private readonly IPlayersService playersService;
        private readonly IGamesService gamesService;

        private int levelCount;
        private ILevel currentLevel;
        private bool MusicOn;
        private SoundPlayer currentSound;

        public MainWindow(IPlayersService playersService, IGamesService gamesService, bool MusicOn)
        {
            InitializeComponent();

            this.playersService = playersService;
            this.gamesService = gamesService;

            answers = new[] { AnswerA, AnswerB, AnswerC, AnswerD };
            engine = new Engine();
            levelCount = 0;
            this.MusicOn = false;

            PlaySound(StartQuestionSound);

            this.MusicOn = MusicOn;

            LoadLevel();
        }

        private void LoadLevel()
        {
            if (levelCount >= engine.Game.Size - 1)
            {
                EndGame end = new EndGame(playersService, gamesService, engine, MusicOn);
                end.ShowDialog();
                Close();
            }
            var regForm = new RegisterPlayer(playersService, gamesService);

            regForm.ShowDialog();

            ResetJokers();

            currentLevel = engine.Game.GetCurrentLevel(levelCount);

            LoadQuestion();

            levelCount++;
        }

        private void LoadQuestion()
        {
            currentLevel.ChangeQuestion();

            if (currentLevel.IsEndOfQuestions)
            {
                YouWin youWin = new YouWin(MusicOn);
                youWin.ShowDialog();
                LoadLevel();
            }

            PlaySound(StartQuestionSound);

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
            string path = prizes[currentLevel.CurrentCount];

            Fifty.BeginInit();
            Fifty.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            Fifty.EndInit();
        }

        private void GenerateAnswers(IQuestion currentQuestion)
        {
            string[] letters = { "A: ", "B: ", "C: ", "D: " };

            currentQuestion.Shuffle();

            for (int i = 0; i < currentQuestion.Answers.Length; i++)
            {
                answers[i].Content = letters[i] + currentQuestion.Answers[i];
                answers[i].Background = Brushes.Black;
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

            PlaySound(FiftyFiftySound);

            Fifty.BeginInit();
            Fifty.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            Fifty.EndInit();

            EliminateTwoAnswers();
        }

        private void PlaySound(string soundDir, bool looping = false)
        {
            this.currentSound = new SoundPlayer(soundDir);

            if (MusicOn)
            {
                if (looping)
                {
                    this.currentSound.PlayLooping();
                }
                else
                {
                    this.currentSound.Play();
                }
            }
        }

        private void EliminateTwoAnswers()
        {
            Thread.Sleep(500);
            foreach (var answer in answers)
            {
                string buttonContent = answer.Content.ToString().Substring(3);

                if (!currentLevel.Current.IsRightAnswer(buttonContent) &&
                    !currentLevel.Current.IsCloseAnswer(buttonContent))
                {
                    answer.Content = string.Empty;
                }
            }
        }

        private void peopleHelper_MouseDown(object sender, MouseEventArgs e)
        {
            string path = AskTheAudienceUsedPicture;


            PlaySound(AskTheAudienceSound, true);

            People.BeginInit();
            People.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            People.EndInit();
        }

        private void AnswerA_Click(object sender, RoutedEventArgs e)
        {
            string answerA = AnswerA.Content.ToString().Substring(3);

            GenerateAnswerOutput(answerA, AnswerA);
        }

        private async void GenerateAnswerOutput(string answer, Button answerButton)
        {
            int miliSec = 7000;
            PlaySound(FinalAnswerSound);

            ColorButton(Brushes.Orange, answerButton);

            await Task.Delay(miliSec);

            if (currentLevel.Current.IsRightAnswer(answer))
            {
                PlaySound(RightAnswerSound);

                ColorButton(Brushes.Green, answerButton);

                await Task.Delay(miliSec);

                //adds a point to the current players score
                gamesService.AddPointToPlayersScore();

                LoadQuestion();
            }
            else
            {
                PlaySound(WrongAnswerSound);

                ColorButton(Brushes.Red, answerButton);

                await Task.Delay(miliSec);

                LoadLevel();
            }
        }

        private async void ColorButton(SolidColorBrush color, Button answerButton)
        {
            int milisec = 200;

            for (int i = 0; i < 15; i++)
            {
                answerButton.Background = Brushes.White;
                answerButton.Foreground = Brushes.Black;
                await Task.Delay(milisec);
                answerButton.Background = color;
                answerButton.Foreground = Brushes.White;
                await Task.Delay(milisec);
            }
        }

        private void AnswerB_Click(object sender, RoutedEventArgs e)
        {
            string answerB = AnswerB.Content.ToString().Substring(3);

            GenerateAnswerOutput(answerB, AnswerB);
        }

        private void AnswerC_Click(object sender, RoutedEventArgs e)
        {
            string answerC = AnswerC.Content.ToString().Substring(3);

            GenerateAnswerOutput(answerC, AnswerC);
        }

        private void AnswerD_Click(object sender, RoutedEventArgs e)
        {
            string answerD = AnswerD.Content.ToString().Substring(3);

            GenerateAnswerOutput(answerD, AnswerD);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.MusicOn)
            {
                this.currentSound.Stop();
            }
            else
            {
                this.currentSound.Play();
            }
            MusicOn = !MusicOn;
        }
    }
}
