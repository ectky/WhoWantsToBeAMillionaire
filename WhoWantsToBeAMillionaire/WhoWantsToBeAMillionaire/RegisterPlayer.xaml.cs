using Game.Data.Services;
using Game.Data.Services.Contracts;
using System.Windows;

namespace WhoWantsToBeAMillionaire
{
    /// <summary>
    /// Interaction logic for RegisterPlayer.xaml
    /// </summary>
    public partial class RegisterPlayer : Window
    {
        private readonly IPlayersService playersService;
        private readonly IGamesService gamesService;

        public RegisterPlayer(IPlayersService playersService, IGamesService gamesService)
        {
            InitializeComponent();

            this.playersService = playersService;
            this.gamesService = gamesService;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;

            playersService.LogPlyerWithUsername(username);
            gamesService.LogGame();

            this.Close();
        }
    }
}
