using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using Yatzy.Logic;
using Yatzy.Logic.Factories;
using Yatzy.Logic.Helpers;
using Yatzy.Logic.Implementations;
using Yatzy.Models;

namespace UnitTests
{
    [TestFixture]
    class GameHandlerTests
    {
        private GameHandler sut;
        IConsoleWrapper consoleWrapper;
        IDiceFacade diceFacade;
        IPlayerFactory playerFactory;

        [SetUp]
        public void SetUp()
        {
            this.consoleWrapper = A.Fake<IConsoleWrapper>();
            this.diceFacade = A.Fake<IDiceFacade>();
            this.playerFactory = A.Fake<IPlayerFactory>();
            sut = new GameHandler(this.consoleWrapper, this.diceFacade, this.playerFactory);
        }

        [Test]
        public void EndTurnShouldPrintDiceResult()
        {
            // Arange
            var diceResult = new List<Dice> { new Dice(1, ""), new Dice(2, "") };

            // Act
            this.sut.EndTurnPrint(diceResult);

            // Assert
            A.CallTo(() => this.diceFacade.PrintDice(diceResult)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void EndTurnShouldPrintAllPlayerCombinationsThatAreNotUsed()
        {
            // Arange
            var combinations = new Dictionary<Combinations, bool> {
                { Combinations.Ones, true },
                { Combinations.Twos, true },
                { Combinations.Threes, true },
                { Combinations.Fours, true },
                { Combinations.Fives, false },
                { Combinations.Sixes, false },
                { Combinations.FullHouse, false }
            };

            var player = new Player { AllowedCombinations = combinations };
            A.CallTo(() => consoleWrapper.GetLine()).Returns("1");

            // Act
            this.sut.EndTurn(A.Dummy<List<Dice>>(), player);

            // Assert
            A.CallTo(() => this.consoleWrapper.Print("Fives 5", true)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => this.consoleWrapper.Print("Sixes 6", true)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => this.consoleWrapper.Print("FullHouse 7", true)).MustNotHaveHappened();
            A.CallTo(() => this.consoleWrapper.Print("Ones 1", true)).MustNotHaveHappened();
        }

        [Test]
        public void EndTurnShouldPrintFullHouseIfItIsAllowedForPlayerAndDiceResultIsFullHouse()
        {
            // Arange
            var diceResult = A.Dummy<List<Dice>>();
            A.CallTo(() => this.diceFacade.IsFullHouse(diceResult)).Returns(true);
            A.CallTo(() => consoleWrapper.GetLine()).Returns("1");

            var combinations = new Dictionary<Combinations, bool> {
                { Combinations.FullHouse, false }
            };
            var player = new Player { AllowedCombinations = combinations };

            // Act
            this.sut.EndTurn(diceResult, player);

            // Assert
            A.CallTo(() => this.consoleWrapper.Print("FullHouse 7", true)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
