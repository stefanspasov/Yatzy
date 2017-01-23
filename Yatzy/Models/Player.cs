using System.Collections.Generic;

namespace Yatzy.Models
{
    public class Player
    {
        public Player()
        {
            AllowedCombinations = new Dictionary<Combinations, bool>
            {
                { Combinations.Ones, false },
                { Combinations.Twos, false },
                { Combinations.Threes, false },
                { Combinations.Fours, false },
                { Combinations.Fives, false },
                { Combinations.Sixes, false },
                { Combinations.FullHouse, false },
            };
        }

        public string PlayerName { get; set; }

        public Dictionary<Combinations, bool> AllowedCombinations { get; set; }

        public int Score { get; set; }
    }
}
