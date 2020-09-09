using System.Threading;

namespace Performance.Parallelism
{
    /// <summary>
    /// O método 'no braço' pode ser otimizado com uma implementação mais 'atual' com ThreadPool.
    /// ThreadPool resolve o problema de alocação de Threads, reaproveitando Threads que já finalizaram
    /// seu processamento.
    /// 
    /// Utilizamos também o conceito de Interlocked que é uma implementação de baixo nível 
    /// para o lock.
    /// </summary>
    public class NumerosPrimosV4
    {
        public static long CalcularPrimosNoIntervalo(long start, long end)
        {
            long result = 0;
            const long chunkSize = 100;
            var completed = 0;
            var allDone = new ManualResetEvent(initialState: false);

            var chunks = (end - start) / chunkSize;

            for (long i = 0; i < chunks; i++)
            {
                var chunkStart = start + i * chunkSize;
                var chunkEnd = (i == (chunks - 1)) ? end : chunkStart + chunkSize;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    for (var number = chunkStart; number < chunkEnd; number++)
                    {
                        if (EhPrimo(number))
                        {
                            Interlocked.Increment(ref result);
                        }
                    }

                    if (Interlocked.Increment(ref completed) == chunks)
                    {
                        allDone.Set();
                    }
                });
            }

            allDone.WaitOne();
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