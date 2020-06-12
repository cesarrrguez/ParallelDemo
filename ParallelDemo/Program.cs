using System;
using System.Linq;

namespace ParallelDemo
{
    internal class Program
    {
        private static void Main()
        {
            ConsoleUtil.PrintSeparatorLine();
            Console.WriteLine("Welcome to Parallel Demo");
            ConsoleUtil.PrintSeparatorLine();

            // Get samples
            var samples = SampleUtil.GetSamples().OrderBy(sample => sample.Id).ToList();

            while (true)
            {
                // Print samples
                Console.WriteLine("\nSamples:\n");
                SampleUtil.PrintSamples(samples);

                // Get sample number or exit
                Console.Write("\n   Enter sample number or '0' to exit: ");
                var line = Console.ReadLine();

                if (line == null || line.Trim() == "0") return;

                var selectedSample = SampleUtil.GetSelectedSample(samples, line);

                if (selectedSample == null) continue;

                try
                {
                    // Print selected sample
                    Console.WriteLine("\n{0}) {1}", selectedSample.Id, selectedSample.Name);
                    ConsoleUtil.PrintSeparatorLine();

                    const int threads = 5;
                    const int iterations = 10;

                    ConsoleUtil.PrintLine("\nStarting sample ...\n", ConsoleColor.Blue);

                    // Run sample
                    selectedSample.Run(threads, iterations);
                }
                catch (Exception e)
                {
                    ConsoleUtil.PrintLine($"\nERROR: {e}", ConsoleColor.DarkYellow);
                }
            }
        }
    }
}
