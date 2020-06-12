using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelDemo.Samples
{
    internal class ManualCancelParallelSample : ISample
    {
        public int Id => 6;
        public string Name => "Manual cancel parallel loop";

        public void Run(int threads, int iterations)
        {
            var rnd = new Random();

            var cancellationTokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions { CancellationToken = cancellationTokenSource.Token, MaxDegreeOfParallelism = threads };

            Console.WriteLine("Enter 'c' to cancel: \n");

            // Run a task so that we can cancel from another thread
            Task.Factory.StartNew(() =>
            {
                if (Console.ReadKey().KeyChar == 'c')
                    cancellationTokenSource.Cancel();
            });

            try
            {
                var result = Parallel.For(0, iterations, parallelOptions, (i, state) =>
                {
                    var delay = rnd.Next(1000, 10000);

                    ConsoleUtil.PrintLine($"START \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId} \t\t Delay {delay}", ConsoleColor.Green);

                    Thread.Sleep(delay);

                    if (parallelOptions.CancellationToken.IsCancellationRequested)
                        ConsoleUtil.PrintLine($"CANCEL \t\t iteration {i} \t\t Thread {Thread.CurrentThread.ManagedThreadId}", ConsoleColor.White, ConsoleColor.Red);

                    parallelOptions.CancellationToken.ThrowIfCancellationRequested();

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
