using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelDemo.Samples
{
    internal class CancelParallelSample : ISample
    {
        public int Id => 4;
        public string Name => "Cancel parallel loop";

        public void Run(int threads, int iterations)
        {
            var rnd = new Random();

            // Get cancel index
            int cancelIndex;
            string line;
            do
            {
                Console.Write($"Enter iteration cancel number (Between 0 and {iterations - 1}): ");
                line = Console.ReadLine();

            } while (!int.TryParse(line, out cancelIndex) || cancelIndex < 0 || cancelIndex > iterations - 1);

            Console.WriteLine("");

            var cancellationTokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions { CancellationToken = cancellationTokenSource.Token, MaxDegreeOfParallelism = threads };

            try
            {
                var result = Parallel.For(0, iterations, parallelOptions, (i, state) =>
                {
                    var delay = rnd.Next(1000, 10000);

                    ConsoleUtil.PrintLine($"START \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId} \t\t Delay {delay}", ConsoleColor.Green);

                    if (i == cancelIndex)
                    {
                        ConsoleUtil.PrintLine($"CANCEL \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId}", ConsoleColor.White, ConsoleColor.Red);
                        cancellationTokenSource.Cancel();
                    }

                    // Check to see whether or not to continue
                    if (state.ShouldExitCurrentIteration) return;

                    Thread.Sleep(delay);

                    ConsoleUtil.PrintLine($"END \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId}", ConsoleColor.Red);
                });

                if (result.IsCompleted)
                {
                    ConsoleUtil.PrintLine("\nAll iterations completed successfully", ConsoleColor.Green);
                }
            }
            catch (AggregateException e)
            {
                ConsoleUtil.PrintLine($"\nERROR: Parallel.For has thrown an AggregateException\n{e}", ConsoleColor.DarkYellow);
            }
            catch (OperationCanceledException e)
            {
                ConsoleUtil.PrintLine($"\nERROR: An iteration has triggered a cancellation\n{e}", ConsoleColor.DarkYellow);
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }
    }
}
