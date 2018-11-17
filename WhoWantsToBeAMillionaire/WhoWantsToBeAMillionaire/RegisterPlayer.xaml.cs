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
using System.Windows.Shapes;
using WhoWantsToBeAMillionaire.Services;
using WhoWantsToBeAMillionaire.Services.Contracts;

namespace WhoWantsToBeAMillionaire
{
    /// <summary>
    /// Interaction logic for RegisterPlayer.xaml
    /// </summary>
    public partial class RegisterPlayer : Window
    {
        private readonly IPlayersService playersService;

        public RegisterPlayer()
        {
            InitializeComponent();

            this.playersService = new PlayersService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;

            //playersService.LogPlyerWithUsername(username);
        }
    }
}
