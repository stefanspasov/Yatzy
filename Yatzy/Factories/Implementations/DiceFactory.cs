using Yatzy.Models;

namespace Yatzy.Factories
{
    class DiceFactory : IDiceFactory
    {
        public Dice Create()
        {
            return new Dice(1);
        }
    }
}
