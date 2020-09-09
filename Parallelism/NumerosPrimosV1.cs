namespace Performance.Parallelism
{
    public class NumerosPrimosV1
    {
        public static long CalcularPrimosNoIntervalo(long start, long end)
        {
            long result = 0;

            for (var number = start; number < end; number++)
            {
                if (EhPrimo(number))
                {
                    result++;
                }
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