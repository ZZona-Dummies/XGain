using BenchmarkDotNet.Running;

namespace Benchmark
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<EndToEnd>();
        }
    }
}