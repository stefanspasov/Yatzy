using System.Collections;
using System.Collections.Generic;
using Yatzy.Models;

namespace Yatzy.Logic.Factories
{
    public interface IPlayerFactory
    {
        IEnumerable<Player> GetPlayers(int playersAmount);
    }
}
