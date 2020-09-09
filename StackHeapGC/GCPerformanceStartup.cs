using BenchmarkDotNet.Attributes;
using StackHeapGC;
using System;

namespace GarbageCollectorPerformance
{
    [MemoryDiagnoser]
    public class GCPerformanceStartup
    {
        private int _executionTimes;

        public GCPerformanceStartup()
        {
            _executionTimes = 1;
        }

        public GCPerformanceStartup(int executionTimes)
        {
            _executionTimes = executionTimes;
        }

        [Benchmark(Baseline = true)]
        public void Internet()
        {
            for (int i = 0; i < _executionTimes; i++)
            {
                if (!Cpf1.Validar("072.302.889-32"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf1.Validar("072.302.889-30"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf1.Validar("333.333.333-33"))
                {
                    throw new Exception("Error!");
                }
            }
        }

        [Benchmark]
        public void ClassStaticArrays()
        {
            for (int i = 0; i < _executionTimes; i++)
            {
                if (!Cpf2.Validar("072.302.889-32"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf2.Validar("072.302.889-30"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf2.Validar("333.333.333-33"))
                {
                    throw new Exception("Error!");
                }
            }
        }

        [Benchmark]
        public void PadLeftToFixedStrings()
        {
            for (int i = 0; i < _executionTimes; i++)
            {
                if (!Cpf3.Validar("072.302.889-32"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf3.Validar("072.302.889-30"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf3.Validar("333.333.333-33"))
                {
                    throw new Exception("Error!");
                }
            }
        }

        [Benchmark]
        public void CharToIntHack()
        {
            for (int i = 0; i < _executionTimes; i++)
            {
                if (!Cpf4.Validar("072.302.889-32"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf4.Validar("072.302.889-30"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf4.Validar("333.333.333-33"))
                {
                    throw new Exception("Error!");
                }
            }
        }

        [Benchmark]
        public void StringBuilder()
        {
            for (int i = 0; i < _executionTimes; i++)
            {
                if (!Cpf5.Validar("072.302.889-32"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf5.Validar("072.302.889-30"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf5.Validar("333.333.333-33"))
                {
                    throw new Exception("Error!");
                }
            }
        }

        [Benchmark]
        public void MemoryToProcessing()
        {
            for (int i = 0; i < _executionTimes; i++)
            {
                if (!Cpf6.Validar("072.302.889-32"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf6.Validar("072.302.889-30"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf6.Validar("333.333.333-33"))
                {
                    throw new Exception("Error!");
                }
            }
        }

        [Benchmark]
        public void OptimizingProcessing()
        {
            for (int i = 0; i < _executionTimes; i++)
            {
                if (!Cpf7.Validar("072.302.889-32"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf7.Validar("072.302.889-30"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf7.Validar("333.333.333-33"))
                {
                    throw new Exception("Error!");
                }
            }
        }

        [Benchmark]
        public void Stackalloc()
        {
            for (int i = 0; i < _executionTimes; i++)
            {
                if (!Cpf8.Validar("072.302.889-32"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf8.Validar("072.302.889-30"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf8.Validar("333.333.333-33"))
                {
                    throw new Exception("Error!");
                }
            }
        }

        [Benchmark]
        public void Struct()
        {
            for (int i = 0; i < _executionTimes; i++)
            {
                if (!Cpf.IsValid("072.302.889-32"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf.IsValid("072.302.889-30"))
                {
                    throw new Exception("Error!");
                }

                if (Cpf.IsValid("333.333.333-33"))
                {
                    throw new Exception("Error!");
                }
            }
        }
    }
}