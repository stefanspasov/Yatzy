using System.Collections.Generic;
using Yatzy.Models;

namespace Yatzy.Logic.Factories.Implementations
{
    public class DiceFactory : IDiceFactory
    {
        static Dictionary<int, string> DiceFaces = new Dictionary<int, string>
        {
            { 1, " -----\n|     |\n|  o  |\n|     |\n -----\n" }, 
            { 2, " -----\n|o    |\n|     |\n|    o|\n -----\n" },
            { 3, " -----\n|o    |\n|  o  |\n|    o|\n -----\n" },
            { 4, " -----\n| o o |\n|     |\n| o o |\n -----\n" },
            { 5, " -----\n| o o |\n|  o  |\n| o o |\n -----\n" },
            { 6, " -----\n| o o |\n| o o |\n| o o |\n -----\n" },
        };

        public Dice Create(int value)
        {
            return new Dice(value, GetDiceFace(value));
        }

        private string GetDiceFace(int value)
        {
            if (DiceFaces.ContainsKey(value))
            {
                return DiceFaces[value];
            }

            throw new KeyNotFoundException(nameof(value));
        }
    }
}
