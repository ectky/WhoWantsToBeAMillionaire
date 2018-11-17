using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    public class Player
    {
        public Player()
        {
            this.Games = new HashSet<Game>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        [NotMapped]
        public int BiggestScore => Games.OrderBy(g => g.Score).FirstOrDefault() == null ? 0 : Games.OrderBy(g => g.Score).First().Score;
    }
}
