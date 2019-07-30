namespace BasketballStatTracker.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int OnePointAttempts { get; set; }
        public int OnePointMakes { get; set; }
        public int TwoPointAttempts { get; set; }
        public int TwoPointMakes { get; set; }
        public int DefensiveRebounds { get; set; }
        public int OffensiveRebounds { get; set; }
        public int Assists { get; set; }
        public int Blocks { get; set; }
        public int Steals { get; set; }
        public int Fouls { get; set; }

        public int Points => (TwoPointMakes * 2) + OnePointMakes;

        public override string ToString()
        {
            return $"FG: {OnePointMakes + TwoPointMakes}/{OnePointAttempts + TwoPointAttempts} " +
                $"Pts: {Points} Reb: {OffensiveRebounds + DefensiveRebounds} " +
                $"Asst: {Assists} Blk: {Blocks} Stl: {Steals} PF: {Fouls}";
        }

        public Player(string name)
        {
            Name = name;
        }
    }
}
