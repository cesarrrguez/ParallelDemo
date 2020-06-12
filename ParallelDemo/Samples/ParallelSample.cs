using System;

namespace ParallelDemo.Samples
{
    internal class ParallelSample : ISample
    {
        public int Id => 2;
        public string Name => "Parallel loop";

        public void Run(int threads, int iterations)
        {
            throw new NotImplementedException();
        }
    }
}
