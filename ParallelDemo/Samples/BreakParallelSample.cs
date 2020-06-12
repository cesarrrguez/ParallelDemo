using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelDemo.Samples
{
    internal class BreakParallelSample : ISample
    {
        public int Id => 3;
        public string Name => "Break parallel loop";

        public void Run(int threads, int iterations)
        {
            var rnd = new Random();

            // Get break index
            int breakIndex;
            string line;
            do
            {
                Console.Write($"Enter iteration break number (Between 0 and {iterations - 1}): ");
                line = Console.ReadLine();

            } while (!int.TryParse(line, out breakIndex) || breakIndex < 0 || breakIndex > iterations - 1);

            Console.WriteLine("");

            var result = Parallel.For(0, iterations, new ParallelOptions { MaxDegreeOfParallelism = threads }, (i, state) =>
            {
                var delay = rnd.Next(1000, 10000);

                ConsoleUtil.PrintLine($"START \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId} \t\t Delay {delay}", ConsoleColor.Green);

                Thread.Sleep(delay);

                if (state.ShouldExitCurrentIteration)
                {
                    if (state.LowestBreakIteration < i)
                        return;
                }

                if (i == breakIndex)
                {
                    ConsoleUtil.PrintLine($"BREAK \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId}", ConsoleColor.White, ConsoleColor.Red);
                    state.Break();
                }

                ConsoleUtil.PrintLine($"END \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId}", ConsoleColor.Red);
            });

            if (result.IsCompleted)
            {
                ConsoleUtil.PrintLine("\nAll iterations completed successfully", ConsoleColor.Green);
            }

            if (result.LowestBreakIteration.HasValue)
                ConsoleUtil.PrintLine($"\nLowest Break Iteration: {result.LowestBreakIteration}", ConsoleColor.Blue);
            else
                ConsoleUtil.PrintLine($"\nNo lowest break iteration", ConsoleColor.Blue);
        }
    }
}
