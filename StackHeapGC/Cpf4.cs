namespace StackHeapGC
{
    public class Cpf4
    {
        private static int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        /// <summary>
        /// Removendo alocações desnecessárias com 'truque' para conversão para inteiro.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Validar(string input)
        {
            var cpf = input.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
            {
                return false;
            }

            if (cpf.Equals("00000000000") ||
                cpf.Equals("11111111111") ||
                cpf.Equals("22222222222") ||
                cpf.Equals("33333333333") ||
                cpf.Equals("44444444444") ||
                cpf.Equals("55555555555") ||
                cpf.Equals("66666666666") ||
                cpf.Equals("77777777777") ||
                cpf.Equals("88888888888") ||
                cpf.Equals("99999999999"))
            {
                return false;
            }

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += (tempCpf[i] - '0') * multiplicador1[i];
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

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (tempCpf[i] - '0') * multiplicador2[i];
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

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}