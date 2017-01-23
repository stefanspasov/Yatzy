using System.Collections.Generic;
using Yatzy.Models;

namespace Yatzy.Logic
{
    interface IDiceFacade
    {
        IList<Dice> RollDice(int diceAmount);

        Dice RollSingleDice();

        bool IsFullHouse(List<Dice> diceResult);

        void PrintDice(IList<Dice> diceList);
    }
}
