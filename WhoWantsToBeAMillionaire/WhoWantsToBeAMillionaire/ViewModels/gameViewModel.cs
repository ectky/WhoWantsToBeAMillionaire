using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWantsToBeAMillionaire.ViewModels
{
    // view model for data grid in end game window
    public class GameViewModel
    {
        public int Rank { get; set; }

        public string Username { get; set; }

        public int Score { get; set; }

        public string PlayedOn { get; set; }
    }
}
