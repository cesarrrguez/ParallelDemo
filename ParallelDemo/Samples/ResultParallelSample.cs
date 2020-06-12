using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelDemo.Samples
{
    internal class ResultParallelSample : ISample
    {
        public int Id => 5;
        public string Name => "Result parallel loop";

        public void Run(int threads, int iterations)
        {
            var rnd = new Random();

            // Result of all threads
            var sharedResult = 0;

            var result = Parallel.For(0, iterations, new ParallelOptions { MaxDegreeOfParallelism = threads }, () => 0, (i, state, local) =>
            {
                var delay = rnd.Next(1000, 10000);

                ConsoleUtil.PrintLine($"START \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId} \t\t Delay {delay}", ConsoleColor.Green);

                Thread.Sleep(delay);

                ConsoleUtil.PrintLine($"END \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId}", ConsoleColor.Red);

                return local + 10;

            }, localState => Interlocked.Add(ref sharedResult, localState));

            if (result.IsCompleted)
            {
                ConsoleUtil.PrintLine("\nAll iterations completed successfully", ConsoleColor.Green);
            }

            // Print result and expected result
            ConsoleUtil.PrintLine($"\nResult: {sharedResult}. Expected {iterations * 10}", ConsoleColor.Blue);
        }
    }
}
