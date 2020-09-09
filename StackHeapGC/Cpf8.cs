using System;

namespace StackHeapGC
{
    public class Cpf8
    {
        /// <summary>
        /// 'Jogando' alocação da Heap para Stack.
        /// Otimizando cálculo para um foreach só.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Validar(string input)
        {
            var length = 0;
            var todosNumerosIguais = true;
            var ultimoDigito = input[0];
            Span<int> cpf = stackalloc int[11];
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

            var soma1 = 0;
            var soma2 = 0;
            for (var posicao = 0; posicao < cpf.Length - 2; posicao++)
            {
                soma1 += cpf[posicao] * (10 - posicao);
                soma2 += cpf[posicao] * (11 - posicao);
            }

            var resto = soma1 % 11;
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

            soma2 += resto * 2;
            resto = soma2 % 11;
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