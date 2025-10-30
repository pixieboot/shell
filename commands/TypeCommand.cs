using src.utility;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace src.commands
{
    public static class TypeCommand
    {
        private static readonly string[] types = ["echo", "exit", "type"];

        private static int FindElement(string[] arr, string input)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (input == arr[i])
                {
                    return 0;
                }
            }
            return -1;
        }

        public static void Validator(string input)
        {
            string substring = input[5..];
            int found = FindElement(types, substring);
            if (found == 0)
            {
                Console.WriteLine($"{substring} is a shell builtin");
            }
            else if (found == -1)
            {
                string result = CheckIfFileExists(substring);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"{substring} not found");
            }
        }

        private static string CheckIfFileExists(string target)
        {
            string notFound = $"{target} not found";
            string? path = Environment.GetEnvironmentVariable("PATH");
            if (path == null)
            {
                return notFound;
            }
            List<string> dirs = [.. path.Split(Path.PathSeparator)];
            foreach (string dir in dirs)
            {
                if (!Directory.Exists(dir))
                {
                    continue;
                }
                foreach (var filePath in Directory.EnumerateFiles(dir))
                {
                    var fileName = Path.GetFileNameWithoutExtension(filePath);

                    if (fileName.Equals(target))
                    {
                        if (!FilePermissionChecker.IsExecutable($"{filePath}"))
                        {
                            continue;
                        }
                        return $"{target} is {filePath}";
                    }
                }
            }
            return notFound;
        }
    }
}