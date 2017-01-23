using System;
using System.Collections.Generic;
using System.Linq;
using Yatzy.Logic;
using Yatzy.Logic.Factories;
using Yatzy.Logic.Helpers;
using Yatzy.Logic.Helpers.Implementations;
using Yatzy.Models;

namespace Yatzy
{
    class DiceFacade : IDiceFacade
    {
        private readonly IDiceFactory diceFactory;
        private readonly IConsoleWrapper consoleWrapper;

        public DiceFacade(IDiceFactory diceFactory, IConsoleWrapper consoleWrapper)
        {
            if (diceFactory == null)
            {
                throw new ArgumentNullException();
            }
            if (consoleWrapper== null)
            {
                throw new ArgumentNullException();
            }
            this.consoleWrapper = consoleWrapper;
            this.diceFactory = diceFactory;
        }

        public IList<Dice> RollDice(int diceAmount)
        {
            var dice = new List<Dice>();
            for (int i = 0; i < diceAmount; i++)
            {
                dice.Add(RollSingleDice());
            }

            return dice;
        }

        public Dice RollSingleDice()
        {
            var diceValue = RandomGenerator.GetRandomDiceValue();
            return diceFactory.Create(diceValue);
        }

        public bool IsFullHouse(List<Dice> diceResult)
        {
            if (diceResult.Count() != 5)
            {
                throw new ArgumentOutOfRangeException(nameof(diceResult));
            }
            diceResult.OrderBy(d => d.Value);
            return ((diceResult.GetRange(0, 3).DistinctBy(d=>d.Value).Count() == 1 && diceResult.GetRange(3, 2).DistinctBy(d => d.Value).Count() == 1)
                || (diceResult.GetRange(0, 2).DistinctBy(d => d.Value).Count() == 1 && diceResult.GetRange(2, 3).DistinctBy(d => d.Value).Count() == 1));
        }

        public void PrintDice(IEnumerable<Dice> diceList)
        {
            foreach (var dice in diceList)
            {
                consoleWrapper.Print(dice.Face, false);
            }
        }
    }
}
