using BenchmarkDotNet.Attributes;

namespace Performance.Parallelism
{
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    public class ParallelismStartup
    {
        private long _start;
        private long _end;

        public ParallelismStartup()
        {
            _start = 200;
            _end = 80_000;
        }

        public ParallelismStartup(long start, long end)
        {
            _start = start;
            _end = end;
        }

        [Benchmark]
        public long WithoutParallelism()
        {
            return NumerosPrimosV1.CalcularPrimosNoIntervalo(_start, _end);
        }

        [Benchmark]
        public long ThreadsWithLock()
        {
            return NumerosPrimosV2.CalcularPrimosNoIntervalo(_start, _end);
        }

        [Benchmark]
        public long ThreadsWithoutLock()
        {
            return NumerosPrimosV3.CalcularPrimosNoIntervalo(_start, _end);
        }

        [Benchmark]
        public long ThreadPool()
        {
            return NumerosPrimosV4.CalcularPrimosNoIntervalo(_start, _end);
        }

        [Benchmark]
        public long TPL()
        {
            return NumerosPrimosV5.CalcularPrimosNoIntervalo(_start, _end);
        }
    }
}