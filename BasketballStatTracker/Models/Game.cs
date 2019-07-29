using System.Collections.Generic;

namespace BasketballStatTracker.Models
{
    public class Game
    {
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
 
        public Game(Team team1, Team team2)
        {
            Team1 = team1;
            Team2 = team2;
        }
    }
}
