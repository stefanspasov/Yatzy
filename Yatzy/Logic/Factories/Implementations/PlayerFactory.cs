using System.Collections;
using System.Collections.Generic;
using Yatzy.Models;

namespace Yatzy.Logic.Factories.Implementations
{
    class PlayerFactory : IPlayerFactory
    {
        public IEnumerable<Player> GetPlayers(int playersAmount)
        {
            var players = new List<Player>();
            for (int i = 0; i < playersAmount; i++)
            {
                players.Add(new Player { PlayerName = $"Player {i}" });
            }

            return players;
        }
    }
}
