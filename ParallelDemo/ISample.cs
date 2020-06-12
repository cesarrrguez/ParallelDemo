namespace ParallelDemo
{
    public interface ISample
    {
        int Id { get; }
        string Name { get; }

        void Run(int threads, int iterations);
    }
}
