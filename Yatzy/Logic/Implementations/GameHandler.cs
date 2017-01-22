using System;
using System.Collections.Generic;
using System.Linq;
using Yatzy.Logic.Factories;
using Yatzy.Models;

namespace Yatzy.Logic.Implementations
{
    class GameHandler
    {
        private readonly IDiceFacade DiceFacade;
        private readonly IPlayerFactory PlayerFactory;
        private IList<Player> Players;
        private const int InitialDice = 5;
        private const int InitialRerolls = 3;

        public GameHandler(IDiceFacade diceFacade, IPlayerFactory playerFactory)
        {
            this.DiceFacade = diceFacade;
            this.PlayerFactory = playerFactory;
        }

        public void Start(int numberOfPlayers)
        {
            Players = this.PlayerFactory.GetPlayers(numberOfPlayers) as IList<Player>;
            for (int i = 0; i < Players.Count(); i++)
            {
                if (i==Players.Count() - 1 && !Players[i].AllowedCombinations.Any(ac => ac.Value == false))
                {
                    break;
                }

                MakeATurn(Players[i]);
            }

            Console.WriteLine("Game ends");
        }

        public void MakeATurn(Player player)
        {
            var result = new List<Dice>();
            var rerollsLeft = InitialRerolls;
            var availableDiceToRoll = InitialDice;

            while(rerollsLeft > 0 && availableDiceToRoll > 0)
            {
                var currentRolledDiceList = this.DiceFacade.RollDice(availableDiceToRoll);

                // TODO Implement IPrinter or logger whatever - something testable
                // TODO Print dice result in another method
                foreach (var dice in currentRolledDiceList)
                {
                    Console.WriteLine(dice.Face + "\n");
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
                                var positions = new int[(availableDiceToRoll - rerollDiceAmount)];
                                Console.WriteLine($"Which dice do you want to keep? By indexes: [0 - {availableDiceToRoll-1}]");
                                for (int i = 0; i < positions.Length; i++)
                                {
                                    // TODO Parse to an int
                                    var dicePosition = Console.ReadKey();
                                    Console.WriteLine();
                                    result.Add(currentRolledDiceList[i]);
                                }

                                availableDiceToRoll -= (availableDiceToRoll - rerollDiceAmount);
                            }
                        }

                        break;
                    }
                }

                rerollsLeft--;
            }

            Console.WriteLine("That is your result");
            foreach (var dice in result)
            {
                Console.WriteLine(dice.Face);
                Console.WriteLine();
            }

            Console.WriteLine("Which score do you want to use?");
            foreach (var item in player.AllowedCombinations.Where(ac => ac.Value == false))
            {
                if (item.Key == Combinations.House)
                {
                    result.OrderBy(d => d.Value);
                    if ((result.GetRange(0, 2).Distinct().Count() == 1 && result.GetRange(3, 4).Distinct().Count() == 1)
                        || (result.GetRange(0, 1).Distinct().Count() == 1 && result.GetRange(2, 4).Distinct().Count() == 1))
                    {
                        Console.WriteLine(item.Key.ToString());

                    }
                }
                else
                {
                    Console.WriteLine(item.Key.ToString());
                }
            }

            var answer = int.Parse(Console.ReadLine());
            if ((Combinations)answer != Combinations.House)
            {
                player.Score = result.Where(d => d.Value == answer).Sum(d => d.Value);
            }
            else
            {
                player.Score = result.Sum(d => d.Value);
            }

            player.AllowedCombinations[(Combinations)answer] = true;
        }
    }
}
