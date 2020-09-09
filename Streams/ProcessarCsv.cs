using BenchmarkDotNet.Attributes;
using Performance;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Streams
{
    [MemoryDiagnoser]
    public class ProcessarCsv
    {
        private const string RatingsPath = "ratings.csv";

        /// <summary>
        /// Aqui trouxemos tudo para a memória! Péssima ideia!
        /// </summary>
        [Benchmark]
        public void ReadAllLines()
        {

            var lines = File.ReadAllLines(RatingsPath);
            var sum = 0d;
            var count = 0;

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts[1] == "110")
                {
                    sum += double.Parse(parts[2], CultureInfo.InvariantCulture);
                    count++;
                }
            }

            Console.WriteLine($"Média do filme Coração Valente é {sum / count} ({count} votos).");
        }

        /// <summary>
        /// Aqui trocamos apenas a leitura para utilização de Stream.
        /// Melhoria de performance e utilização da memória!
        /// </summary>
        [Benchmark]
        public void WithStream()
        {
            var sum = 0d;
            var count = 0;

            string line;

            using (var fs = File.OpenRead(RatingsPath))
            using (var reader = new StreamReader(fs))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');

                    if (parts[1] == "110")
                    {
                        sum += double.Parse(parts[2], CultureInfo.InvariantCulture);
                        count++;
                    }
                }
            }

            Console.WriteLine($"Média do filme Coração Valente é {sum / count} ({count} votos).");
        }

        /// <summary>
        /// Evitando string.Split!
        /// Toda vez que transformo ou modifico uma string, eu crio uma string nova!
        /// Consequentemente a cada split, são geradadas 4 novas strings (4 colunas).
        /// Vamos utilizar Span! 
        /// Recurso mais atual que permite trabalhar com subsets de bytes/arrays
        /// sem necessidade de criar strings novas!
        /// </summary>
        [Benchmark]
        public void AvoidStringSplit()
        {
            var sum = 0d;
            var count = 0;
            string line;

            var lookingFor = "110".AsSpan();

            using (var fs = File.OpenRead(RatingsPath))
            using (var reader = new StreamReader(fs))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    // ignoring the voter id
                    var span = line.AsSpan(line.IndexOf(',') + 1);

                    // movieId
                    var firstCommaPos = span.IndexOf(',');
                    var movieId = span.Slice(0, firstCommaPos);
                    if (!movieId.SequenceEqual(lookingFor))
                    {
                        continue;
                    }

                    // rating
                    span = span.Slice(firstCommaPos + 1);
                    firstCommaPos = span.IndexOf(',');
                    var rating = double.Parse(span.Slice(0, firstCommaPos), provider: CultureInfo.InvariantCulture);

                    sum += rating;
                    count++;
                }
            }

            Console.WriteLine($"Média do filme Coração Valente é {sum / count} ({count} votos).");
        }

        /// <summary>
        /// Tentando eliminar todas alocações da Heap evitando o reader.ReadLine()
        /// que cria uma nova string a cada linha lida do arquivo.
        /// Vamos descer o nível trabalhando com array de bytes!
        /// </summary>
        [Benchmark]
        public void AvoidReadLine()
        {
            var sum = 0d;
            var count = 0;

            var lookingFor = Encoding.UTF8.GetBytes("110").AsSpan();
            /*
             * criando um array de bytes de 1mb para utilizar como buffer de leitura
             */
            var rawBuffer = new byte[1024 * 1024];

            /*
             * abrimos o arquivo
             */
            using (var fs = File.OpenRead(RatingsPath))
            {
                var bytessBuffered = 0;
                var bytessConsumed = 0;

                while (true)
                {
                    /*
                     * começamos a realizar a leitura carregando o buffer, ou seja, 1mb por casa passagem
                     */
                    var bytesRead = fs.Read(rawBuffer, bytessBuffered, rawBuffer.Length - bytessBuffered);

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    bytessBuffered += bytesRead;

                    int linePosition;

                    /*
                     * com uma parte do arquivo carregado na memória, precisamos processá-lo!
                     */
                    do
                    {
                        linePosition = Array.IndexOf(rawBuffer, (byte)'\n', bytessConsumed, bytessBuffered - bytessConsumed);

                        if (linePosition >= 0)
                        {
                            var lineLength = linePosition - bytessConsumed;
                            var line = new Span<byte>(rawBuffer, bytessConsumed, lineLength);
                            bytessConsumed += lineLength + 1;

                            //ignoring the voter id
                            var span = line.Slice(line.IndexOf((byte)',') + 1);

                            // movieId
                            var firstCommaPos = span.IndexOf((byte)',');
                            var movieId = span.Slice(0, firstCommaPos);
                            if (!movieId.SequenceEqual(lookingFor))
                            {
                                continue;
                            }

                            // rating
                            span = span.Slice(firstCommaPos + 1);
                            firstCommaPos = span.IndexOf((byte)',');
                            var rating = double.Parse(Encoding.UTF8.GetString(span.Slice(0, firstCommaPos)), provider: CultureInfo.InvariantCulture);

                            sum += rating;
                            count++;
                        }
                    } while (linePosition >= 0);

                    Array.Copy(rawBuffer, bytessConsumed, rawBuffer, 0, bytessBuffered - bytessConsumed);
                    bytessBuffered -= bytessConsumed;
                    bytessConsumed = 0;
                }
            }

            Console.WriteLine($"Média do filme Coração Valente é {sum / count} ({count} votos).");
        }
    }
}