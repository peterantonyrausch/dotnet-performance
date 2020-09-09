using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Enumerables
{
    public class YieldReturnPractices
    {
        public void Principal(string lookupFileOrDirectory, int index, string folder, params string[] extensions)
        {
            var filenames = EnumeratePaths(folder, extensions);
            var lookupReaded = 0;
            var fileReads = 0;

            foreach (var filename in filenames)
            {
                fileReads++;

                if (filename.EndsWith(lookupFileOrDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    lookupReaded++;

                    if (lookupReaded == index)
                    {
                        Console.WriteLine($"Arquivo '{lookupFileOrDirectory}' de número {index} foi encontrado após {fileReads} leituras.");
                        break;
                    }
                }
            }

            Console.WriteLine($"O total de arquivos lidos foi de {filenames.Count()}.");
        }

        private static IEnumerable<string> EnumeratePaths(string path, params string[] extensions)
        {
            if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                yield return path.Replace("\\", "/");
                yield break;
            }

            var files = Directory.EnumerateFiles(path, "*", new EnumerationOptions
            {
                MatchCasing = MatchCasing.CaseInsensitive,
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            });

            foreach (var file in files)
            {
                if (extensions.Any())
                {
                    var extension = Path.GetExtension(file);
                    if (extensions.Contains(extension))
                    {
                        yield return file.Replace("\\", "/");
                    }
                }
                else
                {
                    yield return file.Replace("\\", "/");
                }
            }
        }
    }
}