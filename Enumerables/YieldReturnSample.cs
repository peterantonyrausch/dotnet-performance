using System;
using System.Collections.Generic;

namespace Enumerables
{
    public class YieldReturnSample
    {
        public void Principal()
        {
            Console.WriteLine("Antes de chamar GetList()");
            var list = GetList();
            Console.WriteLine("Depois de chamar GetList()");

            foreach (var element in list)
            {
                Console.WriteLine($"Antes de imprimir element: {element}");
                Console.WriteLine(element);
                Console.WriteLine($"Depois de imprimir element: {element}");
            }
        }

        public IEnumerable<int> GetList()
        {
            Console.WriteLine("Antes de iniciar o loop for...");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"Antes do yield return ${i}...");
                yield return i;
                Console.WriteLine($"Depois do yield return ${i}...");
            }
            Console.WriteLine("Depois de encerrar o loop for...");
        }
    }
}