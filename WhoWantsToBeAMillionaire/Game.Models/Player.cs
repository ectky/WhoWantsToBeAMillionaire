using System;

namespace Game.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime PlayedOn { get; set; }

        public int Score { get; set; }
    }
}
