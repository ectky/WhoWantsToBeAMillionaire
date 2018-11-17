using Game.App.Entities;
using Game.Data.Services.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WhoWantsToBeAMillionaire.ViewModels;

namespace Game.App
{
    /// <summary>
    /// Interaction logic for EndGame.xaml
    /// </summary>
    public partial class EndGame : Window
    {
        private static string Source = DirectoryProvider.CurrentDirectory();

        private string CloseSound = Source + "close.wav";

        private readonly Engine engine;
        private readonly IPlayersService playersService;
        private readonly IGamesService gamesService;

        private bool MusicOn;
        private SoundPlayer currentSound;


        public EndGame(IPlayersService playersService, IGamesService gamesService, Engine engine, bool MusicOn)
        {
            InitializeComponent();

            this.MusicOn = MusicOn;

            PlaySound(CloseSound);

            this.playersService = playersService;
            this.gamesService = gamesService;
            this.engine = engine;

            // loads leaderboard in data grid 
            dataGridTable.ItemsSource = LoadLeaderBoard();
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

        private List<GameViewModel> LoadLeaderBoard()
        {
            List<Game.Models.Game> games = this.gamesService.GetLastGames(engine.GetCountOfLevels()).OrderByDescending(g => g.Score).ThenByDescending(g => g.PlayedOn).ToList();

            var gameViewModels = games
                .Select(g => new GameViewModel()
                {
                    Username = g.Player.Username,
                    PlayedOn = g.PlayedOn.ToString("dd/MM/yyyy hh:mm:ss.ff",
                                  CultureInfo.InvariantCulture),
                    Score = g.Score,
                    Rank = games.IndexOf(g) + 1
                }).ToList();

            return gameViewModels;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(playersService, gamesService, MusicOn);
            main.ShowDialog();
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.MusicOn)
            {
                this.currentSound.Stop();
            }
            else
            {
                this.currentSound.Play();
            }
            this.MusicOn = !this.MusicOn;
        }
    }
}
