using System;
using System.Collections.Generic;
using System.Linq;
using Yatzy.Logic;

namespace Yatzy.Models
{
    class Game
    {
        private readonly IDiceFacade DiceFacade;

        private IEnumerable<Player> Players { get; set; }

        public Game(IDiceFacade diceFacade)
        {
            this.DiceFacade = diceFacade;
        }

        public void Start(int numberOfPlayers)
        {
            // INTITIALIZE GAME
            Players = new List<Player>();

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
            var result = new List<Dice>();
            var rerollsLeft = 3;
            var availableDiceToRoll = 5;

            while(rerollsLeft > 0 || availableDiceToRoll > 0)
            {
                var currentRolledDiceList = this.DiceFacade.RollDice(availableDiceToRoll);

                // TODO Implement IPrinter or logger whatever - something testable
                // TODO Print dice result in another method
                foreach (var dice in result)
                {
                    Console.Write(dice);
                }

                while (true)
                {
                    Console.WriteLine($"How many dice do you want to reroll? [0-{availableDiceToRoll}]");
                    var input = Console.ReadLine();
                    int rerollDiceAmount;
                    if (int.TryParse(input, out rerollDiceAmount) && availableDiceToRoll >= rerollDiceAmount && rerollDiceAmount >= 0)
                    {
                        if (rerollDiceAmount == 0)
                        {
                            result.AddRange(currentRolledDiceList);
                            rerollsLeft = 0;
                        }
                        else
                        {
                            if (rerollDiceAmount < availableDiceToRoll)
                            {
                                var positions = new int[availableDiceToRoll - rerollDiceAmount];
                                Console.WriteLine($"Which dice do you want to keep? [0 - {availableDiceToRoll}");
                                for (int i = 0; i < positions.Length; i++)
                                {
                                    // TODO Parse to an int
                                    var dicePosition = Console.ReadKey();
                                    result.Add(currentRolledDiceList[i]);
                                }

                                availableDiceToRoll -= rerollDiceAmount;
                            }
                        }

                        break;
                    }
                }

                rerollsLeft--;
            }


            // Roll x dice

            // Ask how many to reroll

            // Roll x dice

            // Which combination to be used 

            // Write down the score and reduce the possible combinations
        }
    }
}
