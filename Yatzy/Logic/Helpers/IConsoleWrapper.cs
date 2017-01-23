namespace Yatzy.Logic.Helpers
{
    public interface IConsoleWrapper
    {
        void Print(string text, bool typeOut = true);

        int GetInt();

        string GetLine();
    }
}
