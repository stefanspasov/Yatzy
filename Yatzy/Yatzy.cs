using System;
using System.Diagnostics;
using System.Threading;
using Yatzy.Logic.Factories.Implementations;
using Yatzy.Logic.Helpers;
using Yatzy.Logic.Helpers.Implementations;

namespace Yatzy
{
    class Yatzy
    {
        static IConsoleWrapper consoleWrapper;

        static void Main(string[] args)
        {
            consoleWrapper = new ConsoleWrapper();

            try
            {
                while (true)
                {
                    // TODO Localize all texts and move them to DB/Resources...
                    consoleWrapper.Print("Do you want to play a game of Yatzy? Yes/No");
                    var input = consoleWrapper.GetLine();
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        input = input.Trim().ToLowerInvariant();
                        if (input == "yes")
                        {
                            consoleWrapper.Print("How many players?");
                            var numberOfPlayers = consoleWrapper.GetInt();
                            var game = GameHandlerFactory.Create();
                            game.Start(numberOfPlayers);
                        }
                        else if(input == "no")
                        {
                            consoleWrapper.Print("Goodbye!");
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
