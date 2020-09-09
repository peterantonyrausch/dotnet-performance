using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using GarbageCollectorPerformance;
using Performance;

namespace StackHeapGC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Benchmark.Run("1.Internet", () => new GCPerformanceStartup(2_000_000).Internet());
            //Benchmark.Run("2.ClassStaticArrays", () => new GCPerformanceStartup(2_000_000).ClassStaticArrays());
            //Benchmark.Run("3.PadLeftToFixedStrings", () => new GCPerformanceStartup(2_000_000).PadLeftToFixedStrings());
            //Benchmark.Run("4.CharToIntHack", () => new GCPerformanceStartup(2_000_000).CharToIntHack());
            //Benchmark.Run("5.StringBuilder", () => new GCPerformanceStartup(2_000_000).StringBuilder());
            //Benchmark.Run("6.MemoryToProcessing", () => new GCPerformanceStartup(2_000_000).MemoryToProcessing());
            //Benchmark.Run("7.OptimizingProcessing", () => new GCPerformanceStartup(2_000_000).OptimizingProcessing());
            //Benchmark.Run("8.Stackalloc", () => new GCPerformanceStartup(2_000_000).Stackalloc());
            //Benchmark.Run("9.Struct", () => new GCPerformanceStartup(2_000_000).Struct());


            //BenchmarkRunner
            //    .Run<GCPerformanceStartup>(
            //        ManualConfig
            //            .Create(DefaultConfig.Instance)
            //            .AddJob(Job.ShortRun.WithRuntime(ClrRuntime.Net48))
            //            .AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core31))
            //    );
        }
    }
}