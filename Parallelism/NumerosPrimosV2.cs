using System;
using System.Threading;

namespace Performance.Parallelism
{
    /// <summary>
    /// Paralelismo no braço. Como se fazia em 1927. ;)
    /// </summary>
    public class NumerosPrimosV2
    {
        public static long CalcularPrimosNoIntervalo(long start, long end)
        {
            long result = 0;
            var lockObject = new object();
            var range = end - start;
            var numberOfThreads = (long)Environment.ProcessorCount;

            var threads = new Thread[numberOfThreads];
            var chunkSize = range / numberOfThreads;

            for (long i = 0; i < numberOfThreads; i++)
            {
                var chunkStart = start + i * chunkSize;
                var chunkEnd = (i == (numberOfThreads - 1)) ? end : chunkStart + chunkSize;
                threads[i] = new Thread(() =>
                {
                    for (var number = chunkStart; number < chunkEnd; number++)
                    {
                        if (EhPrimo(number))
                        {
                            lock (lockObject)
                            {
                                result++;
                            }
                        }
                    }
                });

                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return result;
        }

        private static bool EhPrimo(long numero)
        {
            if (numero == 2)
            {
                return true;
            }

            if (numero % 2 == 0)
            {
                return false;
            }

            for (long divisor = 3; divisor < (numero / 2); divisor += 2)
            {
                if (numero % divisor == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}