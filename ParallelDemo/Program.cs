﻿using System;
using System.Diagnostics;
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

            var stopwatch = new Stopwatch();

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

                    // Get threads
                    int threads;
                    do
                    {
                        Console.Write($"Enter threads number (Between 1 and {Environment.ProcessorCount}): ");
                        line = Console.ReadLine();

                    } while (!int.TryParse(line, out threads) || threads < 1 || threads > Environment.ProcessorCount);

                    // Get iterations
                    int iterations;
                    do
                    {
                        Console.Write("Enter iterations number (More than 0): ");
                        line = Console.ReadLine();

                    } while (!int.TryParse(line, out iterations) || iterations < 1);

                    ConsoleUtil.PrintLine("\nStarting sample ...\n", ConsoleColor.Blue);

                    // Start watch
                    stopwatch.Restart();

                    // Run sample
                    selectedSample.Run(threads, iterations);

                    // Stop watch
                    stopwatch.Stop();

                    // Print sample time
                    ConsoleUtil.PrintLine($"\nFinished sample in {stopwatch.ElapsedMilliseconds} milliseconds", ConsoleColor.Blue);
                }
                catch (Exception e)
                {
                    ConsoleUtil.PrintLine($"\nERROR: {e}", ConsoleColor.DarkYellow);
                }
            }
        }
    }
}
