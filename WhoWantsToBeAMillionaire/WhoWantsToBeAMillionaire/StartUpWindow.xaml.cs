using Game.App.Entities;
using Game.Data.Services;
using Game.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game.App
{
    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        private static string Source = DirectoryProvider.CurrentDirectory();

        private string mainthemeSound = Source + "main theme.wav";
        private readonly SoundPlayer sound;
        private readonly IPlayersService playersService;
        private readonly IGamesService gamesService;

        private bool MusicOn;
        private SoundPlayer currentSound;

        public StartUpWindow()
        {
            InitializeComponent();

            this.MusicOn = true;

            PlaySound(mainthemeSound);

            this.playersService = new PlayersService();
            this.gamesService = new GamesService(playersService);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(playersService, gamesService, MusicOn);
            main.Show();
            this.Close();
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
