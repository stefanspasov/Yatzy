using Yatzy.Models;

namespace Yatzy.Logic.Factories
{
    interface IDiceFactory
    {
        Dice Create(int value);
    }
}
