using System.Threading;
using System.Threading.Tasks;

namespace Performance.Parallelism
{
    /// <summary>
    /// Agora vamos utilizar TPL!
    /// Aqui já trabalhamos com Tasks e uma maneira mais 'nova' de paralelizar processamento.
    /// </summary>
    public class NumerosPrimosV5
    {
        public static long CalcularPrimosNoIntervalo(long start, long end)
        {
            long result = 0;

            Parallel.For(start, end, number =>
            {
                if (EhPrimo(number))
                {
                    Interlocked.Increment(ref result);
                }
            });

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