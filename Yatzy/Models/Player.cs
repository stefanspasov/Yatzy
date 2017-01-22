using System.Collections.Generic;

namespace Yatzy.Models
{
    class Player
    {
        public string PlayerName { get; set; }

        public IEnumerable<Combinations> AllowedCombinations { get; set; }
    }
}
