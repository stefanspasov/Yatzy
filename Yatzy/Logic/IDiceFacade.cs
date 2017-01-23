using System.Collections.Generic;
using Yatzy.Models;

namespace Yatzy.Logic
{
    public interface IDiceFacade
    {
        IList<Dice> RollDice(int diceAmount);

        Dice RollSingleDice();

        bool IsFullHouse(List<Dice> diceResult);

        void PrintDice(IEnumerable<Dice> diceList);
    }
}
