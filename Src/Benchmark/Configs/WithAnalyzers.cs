using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Order;

namespace Benchmark.Configs
{
    [OrderProvider(SummaryOrderPolicy.FastestToSlowest)]
    public class WithAnalyzers : ManualConfig
    {
        public WithAnalyzers()
        {
            Add(StatisticColumn.Mean);
            Add(StatisticColumn.StdErr);
            Add(StatisticColumn.StdDev);
            Add(StatisticColumn.Median);
            Add(CsvMeasurementsExporter.Default);
            Add(RPlotExporter.Default);
            Add(MemoryDiagnoser.Default);
        }
    }
}
