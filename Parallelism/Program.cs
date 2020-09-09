using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Performance;
using Performance.Parallelism;

namespace Parallelism
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Benchmark.Run("1.WithoutParallelism", () => new ParallelismStartup().WithoutParallelism(200, 200_000));
            //Benchmark.Run("2.ThreadsWithLock", () => new ParallelismStartup().ThreadsWithLock(200, 200_000));
            //Benchmark.Run("3.ThreadsWithoutLock", () => new ParallelismStartup().ThreadsWithoutLock(200, 200_000));
            //Benchmark.Run("4.ThreadPool", () => new ParallelismStartup().ThreadPool(200, 200_000));
            //Benchmark.Run("5.TPL", () => new ParallelismStartup().TPL(200, 200_000));

            //BenchmarkRunner
            //   .Run<ParallelismStartup>(
            //       ManualConfig
            //           .Create(DefaultConfig.Instance)
            //           .AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core31))
            //   );


            // CONCLUSÕES
            // 1. Se a máquina tem vários processadores e você não está utilizando, está disperdiçando processamento;
            // 2. Existem recursos de linguagem novos e não faz sentido utilizar Threads no modelo antigo (na unha);
            // 3. ThreadPool ainda são performáticos, mas as implementações mais novas ainda são a melhor solução;
            // 4. O LOCK que normalmente é considerado vilão de perfomrnace, não fez diferença alguma no exemplo;
        }
    }
}