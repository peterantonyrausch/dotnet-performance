namespace StackHeapGC
{
    public class Cpf7
    {
        private static int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        /// <summary>
        /// Otimizando os processamentos e criando um array de inteiro para ObterDigito.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Validar(string input)
        {
            var length = 0;
            var todosNumerosIguais = true;
            var ultimoDigito = input[0];
            var cpf = new int[11];
            foreach (var c in input)
            {
                if (char.IsNumber(c))
                {
                    if (length > 10)
                    {
                        return false;
                    }

                    cpf[length] = c - '0';
                    todosNumerosIguais &= ultimoDigito == c;
                    ultimoDigito = c;

                    length++;
                }
            }

            if (length != 11)
            {
                return false;
            }

            if (todosNumerosIguais)
            {
                return false;
            }

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += cpf[i] * multiplicador1[i];
            }

            int resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            if (resto != cpf[9])
            {
                return false;
            }

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += cpf[i] * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            return resto == cpf[10];
        }
    }
}