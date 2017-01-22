namespace Yatzy.Models
{
    class Dice
    {
        public readonly int Value;

        public Dice(int value)
        {
            if (value > 6 || value < 1)
            {
                throw new System.ArgumentOutOfRangeException(nameof(value));
            }

            this.Value = value;
        }
    }
}
