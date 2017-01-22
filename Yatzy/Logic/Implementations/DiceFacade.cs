using System.Collections.Generic;
using Yatzy.Logic;
using Yatzy.Logic.Factories;
using Yatzy.Logic.Helpers;
using Yatzy.Models;

namespace Yatzy
{
    class DiceFacade : IDiceFacade
    {
        private readonly IDiceFactory DiceFactory;

        public DiceFacade(IDiceFactory diceFactory)
        {
            DiceFactory = diceFactory;
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
            return DiceFactory.Create(diceValue);
        }
    }
}
