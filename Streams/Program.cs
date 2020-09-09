using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Performance;
using Streams;

namespace StackHeapGC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Benchmark.Run("1.ReadAllLines", () => new ProcessarCsv().ReadAllLines());
            //Benchmark.Run("2.WithStream", () => new ProcessarCsv().WithStream());
            //Benchmark.Run("3.AvoidStringSplit", () => new ProcessarCsv().AvoidStringSplit());
            //Benchmark.Run("4.AvoidReadLine", () => new ProcessarCsv().AvoidReadLine());

            //BenchmarkRunner
            //  .Run<ProcessarCsv>(
            //      ManualConfig
            //          .Create(DefaultConfig.Instance)
            //          .AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core31))
            //  );
        }
    }
}