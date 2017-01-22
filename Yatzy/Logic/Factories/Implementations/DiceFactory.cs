using System.Collections.Generic;
using Yatzy.Models;

namespace Yatzy.Factories
{
    class DiceFactory : IDiceFactory
    {
        static Dictionary<int, string> DiceFaces = new Dictionary<int, string>
        {
            { 1, "[     ]\n[  o  ]\n[     ]" }, 
            { 2, "[     ]\n[ o o ]\n[     ]" },
            { 3, "[  o  ]\n[ o o ]\n[     ]" },
            { 4, "[ o o ]\n[     ]\n[ o o ]" },
            { 5, "[ o o ]\n[  o  ]\n[ o o ]" },
            { 6, "[ o o ]\n[ o o ]\n[ o o ]" },
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
