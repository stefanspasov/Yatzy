using System;
using System.Diagnostics;
using System.Threading;
using Yatzy.Logic.Factories.Implementations;
using Yatzy.Logic.Implementations;

namespace Yatzy
{
    class Yatzy
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Do you want to play a game of Yatzy? Yes/No");
                    var input = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        input = input.Trim().ToLowerInvariant();
                        if (input == "yes")
                        {
                            Console.WriteLine("How many players?");
                            var numberOfPlayersInput = Console.ReadLine();
                            int numberOfPlayers;
                            if (int.TryParse(numberOfPlayersInput, out numberOfPlayers) && numberOfPlayers > 0)
                            {
                                var game = new GameHandler(new DiceFacade(new DiceFactory()), new PlayerFactory());
                                game.Start(numberOfPlayers);
                            }
                            else
                            {
                                Console.WriteLine("Players should be a positive integer.");
                            }
                        }
                        else if(input == "no")
                        {
                            Console.WriteLine("Goodbye!");
                            Thread.Sleep(1000);
                            Environment.Exit(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Suppressed exception.", ex);
            }
        }
    }
}
