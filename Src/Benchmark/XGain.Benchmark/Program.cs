using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace XGain.Benchmark
{
    public class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<RecivingTime>();
            Console.ReadKey();
        }
    }
}
