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
        private string PrizePicture = Source + "question.png";
        private string PrizeSelectedPicture = Source + "question.png";

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

            LoadPrizes();

            LoadQuestion();

            levelCount++;
        }

        private void LoadPrizes()
        {
            PanelImages.Children.Clear();
            PanelLabels.Children.Clear();

            for (int i = 0; i < engine.Prizes.Length; i++)
            {                
                Label label = new Label();
                label.Content = engine.Prizes[i];
                label.Width = 150;
                label.FontSize = 18;
                label.Height = 45;
                label.Foreground = Brushes.White;

                PanelLabels.Children.Add(label);
                
                //----------------< add_Image() >----------------
                Image newImage = new Image();

                //< source >
                BitmapImage src = new BitmapImage();
                src.BeginInit();

                if (currentLevel.CurrentCount == i)
                {
                    src.UriSource = new Uri(PrizePicture, UriKind.RelativeOrAbsolute);
                }
                else
                {
                    src.UriSource = new Uri(PrizeSelectedPicture, UriKind.RelativeOrAbsolute);
                }
                
                src.EndInit();
                newImage.Source = src;
                //</ source >

                newImage.Stretch = Stretch.Uniform;
                newImage.Height = 45;
                newImage.Width = 550;

                //< add >
                PanelImages.Children.Add(newImage);

            }
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

            LoadPrizes();

            QuestionLabel.Text = currentLevel.Current.QuestionText;

            GenerateAnswers(currentLevel.Current);
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

            FiftyPictureWin.BeginInit();
            FiftyPictureWin.Source = new BitmapImage(new Uri(FiftyPicture, UriKind.RelativeOrAbsolute));
            FiftyPictureWin.EndInit();

            path = AskTheAudiencePicture;

            People.BeginInit();
            People.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            People.EndInit();
        }

        private void fiftyHelper_MouseDown(object sender, MouseEventArgs e)
        {
            string path = FiftyUsedPicture;

            PlaySound(FiftyFiftySound);

            FiftyPictureWin.BeginInit();
            FiftyPictureWin.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            FiftyPictureWin.EndInit();

            EliminateTwoAnswers();
        }

        private void PlaySound(string soundDir, bool looping = false)
        {
            currentSound = new SoundPlayer(soundDir);

            if (MusicOn)
            {
                if (looping)
                {
                    currentSound.PlayLooping();
                }
                else
                {
                    currentSound.Play();
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
            int miliSec = 8000;
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

            for (int i = 0; i < 13; i++)
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
            if (MusicOn)
            {
                currentSound.Stop();
            }
            else
            {
                currentSound.Play();
            }
            MusicOn = !MusicOn;
        }
    }
}
