using System;
using System.Linq;

namespace ParallelDemo
{
    internal static class ConsoleUtil
    {
        internal static void PrintLine(string line, ConsoleColor color = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = background;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        internal static void PrintSeparatorLine(char separator = '-', int count = 40)
        {
            var line = new string(Enumerable.Repeat(separator, count).ToArray());
            Console.WriteLine(line);
        }
    }
}
