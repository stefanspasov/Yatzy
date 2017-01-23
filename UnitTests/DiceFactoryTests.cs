using NUnit.Framework;
using Yatzy.Logic.Factories.Implementations;

namespace YatzyTests
{
    [TestFixture]
    public class DiceFactoryTests
    {
        private DiceFactory sut;

        [SetUp]
        public void SetUp()
        {
            sut = new DiceFactory();
        }

        [Test]
        public void CreateShouldReturnADice()
        {
            // Arange
            var value = 2;

            // Act
            var dice = sut.Create(value);

            // Assert
            Assert.AreEqual(value, dice.Value);
        }
    }
}
