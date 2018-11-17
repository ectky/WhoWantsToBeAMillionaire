using Game.Data.Services.Contracts;
using System;
using System.Collections.Generic;
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

namespace Game.App
{
    /// <summary>
    /// Interaction logic for EndGame.xaml
    /// </summary>
    public partial class EndGame : Window
    {
        private readonly IPlayersService playersService;
        private readonly IGamesService gamesService;

        public EndGame(IPlayersService playersService, IGamesService gamesService)
        {
            InitializeComponent();
            SoundPlayer sound = new SoundPlayer
                (@"D:\VSProjects\WhoWantsToBecomeAMillionaire\Game.App\Resources\close.wav");
            sound.Play();

            this.playersService = playersService;
            this.gamesService = gamesService;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(playersService, gamesService);
            main.ShowDialog();
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
