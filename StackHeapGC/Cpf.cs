using System;

namespace StackHeapGC
{
    public struct Cpf
    {
        public string Numero { get; }

        private Cpf(string text)
        {
            if (text == null)
            {
                Numero = null;
                return;
            }

            if (IsValid(text))
            {
                Numero = text;
                return;
            }

            throw new Exception("Número de CPF inválido.");
        }

        public static Cpf Parse(string text) => new Cpf(text);

        public static bool TryParse(string text, out Cpf cpf)
        {
            try
            {
                cpf = Parse(text);
                return true;
            }
            catch
            {
                cpf = null;
                return false;
            }
        }

        public static implicit operator Cpf(string text) => Parse(text);

        public static bool IsValid(string input)
        {
            Span<int> cpfArray = stackalloc int[11];
            var count = 0;
            var todosNumerosIguais = true;
            var ultimoDigito = input[0];

            foreach (var c in input)
            {
                if (char.IsDigit(c))
                {
                    if (count > 10)
                    {
                        return false;
                    }
                    cpfArray[count] = c - '0';
                    count++;

                    todosNumerosIguais &= ultimoDigito == c;
                    ultimoDigito = c;
                }
            }

            if (count != 11)
            {
                return false;
            }

            if (todosNumerosIguais)
            {
                return false;
            }

            var totalDigito1 = 0;
            var totalDigito2 = 0;

            for (var posicao = 0; posicao < cpfArray.Length - 2; posicao++)
            {
                totalDigito1 += cpfArray[posicao] * (10 - posicao);
                totalDigito2 += cpfArray[posicao] * (11 - posicao);
            }

            var mod1 = totalDigito1 % 11;
            if (mod1 < 2)
            {
                mod1 = 0;
            }
            else
            {
                mod1 = 11 - mod1;
            }

            if (cpfArray[9] != mod1)
            {
                return false;
            }

            totalDigito2 += mod1 * 2;

            var mod2 = totalDigito2 % 11;
            if (mod2 < 2)
            {
                mod2 = 0;
            }
            else
            {
                mod2 = 11 - mod2;
            }

            return cpfArray[10] == mod2;
        }

        public override string ToString()
        {
            return Numero;
        }
    }
}