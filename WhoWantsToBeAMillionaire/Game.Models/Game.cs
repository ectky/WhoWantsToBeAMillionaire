using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    public class Game
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        public int Score { get; set; }

        public DateTime PlayedOn { get; set; }
    }
}
