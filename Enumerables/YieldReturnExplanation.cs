using System;
using System.Collections;
using System.Collections.Generic;

namespace Enumerables
{
    public class YieldReturnExplanation
    {
        public void Principal()
        {
            Console.WriteLine("Antes de chamar GetList()");
            var list = GetList();
            Console.WriteLine("Depois de chamar GetList()");

            //foreach (var element in list)
            //{
            //    Console.WriteLine($"Antes de imprimir element: {element}");
            //    Console.WriteLine(element);
            //    Console.WriteLine($"Depois de imprimir element: {element}");
            //}

            /*
             * O que realmente acontece é que o compilador substitui 
             * a implementação acima por esta abaixo
             */
            using (var enumerator = list.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var element = enumerator.Current;
                    Console.WriteLine($"Antes de imprimir element: {element}");
                    Console.WriteLine(element);
                    Console.WriteLine($"Depois de imprimir element: {element}");
                }
            }
        }

        public IEnumerable<int> GetList()
        {
            return new MyEnumerable();
        }

        public class MyEnumerable : IEnumerable<int>, IDisposable
        {
            public void Dispose()
            {
            }

            public IEnumerator<int> GetEnumerator() => new MyEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new MyEnumerator();
            }
        }

        public class MyEnumerator : IEnumerator<int>
        {
            public int Current { get; private set; } = -1;

            object IEnumerator.Current => Current;

            public MyEnumerator()
            {
                Console.WriteLine("Antes de iniciar o loop for...");
            }

            public void Dispose()
            {
                Console.WriteLine("Depois de encerrar o loop for...");
            }

            public bool MoveNext()
            {
                if (Current >= 0)
                {
                    Console.WriteLine($"Depois do yield return ${Current}...");
                }

                Current++;

                if (Current >= 4)
                {
                    return false;
                }

                Console.WriteLine($"Antes do yield return ${Current}...");

                return true;
            }

            public void Reset()
            {
                Current = -1;
            }
        }
    }
}