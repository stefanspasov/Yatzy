using System;

namespace Yatzy.Logic.Helpers.Implementations
{
    class ConsoleWrapper : IConsoleWrapper
    {
        public string GetLine()
        {
            return Console.ReadLine();
        }

        public int GetInt()
        {
            while (true)
            {
                var input = Console.ReadLine();
                int number;
                if (int.TryParse(input, out number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Enter a positive integer!");
                }
            }
        }

        public void Print(string text, bool typeOut = true)
        {
            if (typeOut)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    Console.Write(text[i]);
                    System.Threading.Thread.Sleep(30);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(text);
            }
        }
    }
}

