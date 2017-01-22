using System.Collections.Generic;
using Yatzy.Models;

namespace Yatzy.Logic
{
    interface IDiceFacade
    {
        IEnumerable<Dice> RollDice(int diceAmount);

        Dice RollSingleDice();
    }
}
