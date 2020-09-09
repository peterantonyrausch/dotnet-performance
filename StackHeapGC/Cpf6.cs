using System.Text;
using System.Threading;

namespace StackHeapGC
{
    public class Cpf6
    {
        private static int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        /// <summary>
        /// Trocando todas alocações de string por processamento.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Validar(string input)
        {
            var length = 0;
            foreach (var c in input)
            {
                if (char.IsNumber(c))
                {
                    if (length > 10)
                    {
                        return false;
                    }

                    length++;
                }
            }

            if (length != 11)
            {
                return false;
            }

            var todosNumerosIguais = true;
            var ultimoDigito = input[0];
            foreach (var c in input)
            {
                if (char.IsNumber(c))
                {
                    todosNumerosIguais &= ultimoDigito == c;
                    ultimoDigito = c;
                }
            }

            if (todosNumerosIguais)
            {
                return false;
            }

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += ObterDigito(input, i) * multiplicador1[i];
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

            if (resto != ObterDigito(input, 9))
            {
                return false;
            }

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += ObterDigito(input, i) * multiplicador2[i];
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

            return resto == ObterDigito(input, 10);
        }

        private static int ObterDigito(string input, int posicao)
        {
            int count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    if (count == posicao)
                    {
                        return input[i] - '0';
                    }
                    count++;
                }
            }

            return 0;
        }
    }
}