using System;

namespace ParallelDemo.Samples
{
    internal class SequentialSample : ISample
    {
        public int Id => 1;
        public string Name => "Sequential loop";

        public void Run(int threads, int iterations)
        {
            throw new NotImplementedException();
        }
    }
}
