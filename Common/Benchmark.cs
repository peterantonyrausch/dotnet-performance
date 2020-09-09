using System;
using System.Diagnostics;

namespace Performance
{
    public static class Benchmark
    {
        public static void Run(string executionName, Action action)
        {
            var sw = new Stopwatch();
            var before0 = GC.CollectionCount(0);
            var before1 = GC.CollectionCount(1);
            var before2 = GC.CollectionCount(2);
            var startMemory = CalculateMemory();

            Console.WriteLine($"--- starting {executionName} -".PadRight(30, '-'));

            sw.Start();
            action();
            sw.Stop();

            Console.WriteLine($"--- finished {executionName} -".PadRight(30, '-'));

            Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"GC Gen #0 : {GC.CollectionCount(0) - before0}");
            Console.WriteLine($"GC Gen #1 : {GC.CollectionCount(1) - before1}");
            Console.WriteLine($"GC Gen #2 : {GC.CollectionCount(2) - before2}");
            Console.WriteLine($"Memory: {CalculateMemory() - startMemory} mb");

            Console.WriteLine("-".PadRight(30, '-'));
            Console.WriteLine();
        }

        private static long CalculateMemory() => Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024;
    }
}