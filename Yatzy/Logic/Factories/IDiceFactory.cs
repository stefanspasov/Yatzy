using Yatzy.Models;

namespace Yatzy.Factories
{
    interface IDiceFactory
    {
        Dice Create(int value);
    }
}
