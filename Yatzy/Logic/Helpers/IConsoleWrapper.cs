namespace Yatzy.Logic.Helpers
{
    interface IConsoleWrapper
    {
        void Print(string text, bool typeOut = true);

        int GetInt();

        string GetLine();
    }
}
