using System.Collections;
using System.Collections.Generic;
using Yatzy.Models;

namespace Yatzy.Logic.Factories
{
    interface IPlayerFactory
    {
        IEnumerable<Player> GetPlayers(int playersAmount);
    }
}
