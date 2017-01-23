using Yatzy.Logic.Helpers.Implementations;
using Yatzy.Logic.Implementations;

namespace Yatzy.Logic.Factories.Implementations
{
    class GameHandlerFactory
    {
        public static GameHandler Create()
        {
            var consoleWrapper = new ConsoleWrapper();
            return new GameHandler(consoleWrapper, new DiceFacade(new DiceFactory(), consoleWrapper), new PlayerFactory());
        }
    }
}
