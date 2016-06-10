using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace XGain.Benchmark
{
    public class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<RecivingTime>(DefaultConfig.Instance.RemoveBenchmarkFiles());
        }
    }
}
