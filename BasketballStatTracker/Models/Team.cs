using System.Collections.Generic;
using System.Linq;

namespace BasketballStatTracker.Models
{
    public class Team
    {
        public List<Player> Players { get; set; }
        public int Points => Players.Sum(x => x.Points);

        public Team(List<Player> players)
        {
            Players = players;
        }
    }
}
