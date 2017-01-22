using System;

namespace Yatzy.Logic.Helpers
{
    class RandomGenerator
    {
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();

        public static int GetRandomDiceValue()
        {
            lock (syncLock)
            { 
                return getrandom.Next(1, 6);
            }
        }
    }
}
