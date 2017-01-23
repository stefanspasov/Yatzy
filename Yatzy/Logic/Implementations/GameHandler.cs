using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Yatzy.Logic.Factories;
using Yatzy.Logic.Helpers;
using Yatzy.Models;

namespace Yatzy.Logic.Implementations
{
    public class GameHandler
    {
        private readonly IDiceFacade diceFacade;
        private readonly IPlayerFactory playerFactory;
        private IList<Player> Players;
        private IConsoleWrapper consoleWrapper;
        private const int InitialDice = 5;
        private const int InitialRerolls = 3;

        public GameHandler(IConsoleWrapper consoleWrapper, IDiceFacade diceFacade, IPlayerFactory playerFactory)
        {
            if (consoleWrapper == null)
            {
                throw new ArgumentNullException(nameof(consoleWrapper));
            }
            if (diceFacade == null)
            {
                throw new ArgumentNullException(nameof(diceFacade));
            }
            if (playerFactory == null)
            {
                throw new ArgumentNullException(nameof(playerFactory));
            }
            this.consoleWrapper = consoleWrapper;
            this.diceFacade = diceFacade;
            this.playerFactory = playerFactory;
        }

        public void Start(int numberOfPlayers)
        {
            Players = this.playerFactory.GetPlayers(numberOfPlayers) as IList<Player>;
            for (int i = 0; i < Players.Count(); i++)
            {
                consoleWrapper.Print($"{Players[i].PlayerName}'s turn! Score:{Players[i].Score}\n");
                Thread.Sleep(500);
                MakeATurn(Players[i]);
                if (i == Players.Count() - 1 && Players[i].AllowedCombinations.Any(ac => ac.Value == false))
                {
                    i = -1;
                }
            }

            var winner = Players.OrderByDescending(p => p.Score).First();
            consoleWrapper.Print($"Game over. Winner is {winner.PlayerName} with score: {winner.Score}");
        }

        public void MakeATurn(Player player)
        {
            var diceResult = new List<Dice>();
            var rerollsLeft = InitialRerolls;
            var availableDiceToRoll = InitialDice;
            IList<Dice> currentRolledDiceList = null;

            while (rerollsLeft > 0 && availableDiceToRoll > 0)
            {
                currentRolledDiceList = this.diceFacade.RollDice(availableDiceToRoll);
                this.diceFacade.PrintDice(currentRolledDiceList);

                while (true)
                {
                    consoleWrapper.Print($"How many dice do you want to reroll? [0-{availableDiceToRoll}]");
                    var rerollDiceAmount = consoleWrapper.GetInt();
                    if (availableDiceToRoll >= rerollDiceAmount)
                    {
                        if (rerollDiceAmount == 0)
                        {
                            diceResult.AddRange(currentRolledDiceList);
                            rerollsLeft = 0;
                        }
                        else
                        {
                            if (rerollDiceAmount < availableDiceToRoll)
                            {
                                var positions = new int[(availableDiceToRoll - rerollDiceAmount)];
                                consoleWrapper.Print($"Which dice do you want to keep? By indexes: [0 - {availableDiceToRoll-1}]");
                                for (int i = 0; i < positions.Length; i++)
                                {
                                    var dicePosition = consoleWrapper.GetInt();
                                    diceResult.Add(currentRolledDiceList[dicePosition]);
                                }

                                availableDiceToRoll -= (availableDiceToRoll - rerollDiceAmount);
                            }
                        }

                        break;
                    }
                }

                rerollsLeft--;
            }

            if (diceResult.Count() < InitialDice)
            {
                diceResult.AddRange(currentRolledDiceList);
            }

            EndTurn(diceResult, player);
        }

        public void EndTurn(List<Dice> diceResult, Player player)
        {
            EndTurnPrint(diceResult);
            foreach (var item in player.AllowedCombinations.Where(ac => ac.Value == false))
            {
                if ((item.Key == Combinations.FullHouse && this.diceFacade.IsFullHouse(diceResult)) || item.Key != Combinations.FullHouse)
                {
                    consoleWrapper.Print(item.Key.ToString() + " " + (int)item.Key);
                }
            }

            var answer = (Combinations)int.Parse(consoleWrapper.GetLine());
            player.Score += CalculateScore(answer, diceResult);
            player.AllowedCombinations[answer] = true;
        }

        // TODO Move this to another class
        public int CalculateScore(Combinations combinations, IEnumerable<Dice> diceResult)
        {
            if (combinations != Combinations.FullHouse)
            {
                return diceResult.Where(d => d.Value == (int)combinations).Sum(d => d.Value);
            }
            else
            {
                return diceResult.Sum(d => d.Value);
            }
        }

        public void EndTurnPrint(IEnumerable<Dice> diceResult)
        {
            consoleWrapper.Print("That is your result:");
            this.diceFacade.PrintDice(diceResult);
            consoleWrapper.Print("Which score do you want to use?");
        }
    }
}
