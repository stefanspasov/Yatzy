using System.Collections.Generic;
using System.Linq;

namespace Yatzy.Models
{
    class Game
    {
        public IEnumerable<Player> Players { get; set; }

        public Game(int numberOfPlayers)
        {
            Players = new List<Player>();
        }

        public void Start()
        {
            foreach (var player in this.Players)
            {
                if (player.AllowedCombinations.Any())
                {
                    MakeATurn(player);
                }
            }

            // Game end
        }

        public void MakeATurn(Player player)
        {

            // TODO Implement IPrinter or logger whatever - something testable
            // Roll 5 dice

            // Ask how many to reroll

            // Roll x dice

            // Ask how many to reroll

            // Roll x dice

            // Which combination to be used 

            // Write down the score and reduce the possible combinations
        }
    }
}
