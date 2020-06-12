using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelDemo.Samples
{
    internal class ParallelSample : ISample
    {
        public int Id => 2;
        public string Name => "Parallel loop";

        public void Run(int threads, int iterations)
        {
            var rnd = new Random();

            var result = Parallel.For(0, iterations, new ParallelOptions { MaxDegreeOfParallelism = threads }, (i, state) =>
            {
                var delay = rnd.Next(1000, 10000);

                ConsoleUtil.PrintLine($"START \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId} \t\t Delay {delay}", ConsoleColor.Green);

                Thread.Sleep(delay);

                ConsoleUtil.PrintLine($"END \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId}", ConsoleColor.Red);
            });

            if (result.IsCompleted)
            {
                ConsoleUtil.PrintLine("\nAll iterations completed successfully", ConsoleColor.Green);
            }
        }
    }
}
