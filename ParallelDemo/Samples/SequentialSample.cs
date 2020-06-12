using System;
using System.Threading;

namespace ParallelDemo.Samples
{
    internal class SequentialSample : ISample
    {
        public int Id => 1;
        public string Name => "Sequential loop";

        public void Run(int threads, int iterations)
        {
            var rnd = new Random();

            for (var i = 0; i < iterations; i++)
            {
                var delay = rnd.Next(1000, 10000);

                ConsoleUtil.PrintLine($"START \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId} \t\t Delay {delay}", ConsoleColor.Green);

                Thread.Sleep(delay);

                ConsoleUtil.PrintLine($"END \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId}", ConsoleColor.Red);
            }

            ConsoleUtil.PrintLine("\nAll iterations completed successfully", ConsoleColor.Green);
        }
    }
}
