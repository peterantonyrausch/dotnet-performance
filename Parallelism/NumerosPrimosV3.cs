using System;
using System.Linq;
using System.Threading;

namespace Performance.Parallelism
{
    /// <summary>
    /// Reza a lenda que o lock em processamento paralelo é o grande vilão.
    /// Vamos removê-lo!
    /// </summary>
    public class NumerosPrimosV3
    {
        public static long CalcularPrimosNoIntervalo(long inicio, long fim)
        {
            var locker = new object();

            var range = fim - inicio;
            var numeroThreads = (long)Environment.ProcessorCount;

            var resultados = new long[numeroThreads];
            var threads = new Thread[numeroThreads];
            var chunkSize = range / numeroThreads;

            for (var i = 0; i < numeroThreads; i++)
            {
                var chunkStart = inicio + i * chunkSize;
                var chunkEnd = (i == (numeroThreads - 1)) ? fim : chunkStart + chunkSize;
                var current = i;
                threads[i] = new Thread(() =>
                {
                    resultados[current] = 0;
                    for (var numero = chunkStart; numero < chunkEnd; numero++)
                    {
                        if (EhPrimo(numero))
                        {
                            resultados[current]++;
                        }
                    }
                });

                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return resultados.Sum();
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