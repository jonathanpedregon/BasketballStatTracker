using System.Collections.Generic;

namespace BasketballStatTracker.Models
{
    public class Team
    {
        public List<Player> Players { get; set; }
        public int Score { get; set; }

        public Team(List<Player> players)
        {
            Players = players;
        }
    }
}
