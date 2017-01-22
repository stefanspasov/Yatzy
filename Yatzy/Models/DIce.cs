namespace Yatzy.Models
{
    class Dice
    {
        public readonly int Value;
        public readonly string Face;

        public Dice(int value, string face)
        {
            if (value > 6 || value < 1)
            {
                throw new System.ArgumentOutOfRangeException(nameof(value));
            }

            this.Value = value;
            this.Face = face;
        }
    }
}
