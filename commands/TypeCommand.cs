using shell.utility;

namespace shell.commands
{
    internal static class TypeCommand
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
                List<string>? result = FileChecker.CheckIfFileExists(substring);
                if (result != null)
                {
                    string[]? filterResult = FilterList(substring, result);
                    if (filterResult != null)
                    {
                        // writes out {input} is {file path}
                        Console.WriteLine($"{substring} is {filterResult[1]}");
                    }
                    else
                    {
                        Console.WriteLine($"{substring} not found");
                    }
                }
                else
                {
                    Console.WriteLine($"{substring} not found");
                }
            }
            else
            {
                Console.WriteLine($"{substring} not found");
            }
        }

        private static string[]? FilterList(string target, List<string> list)
        {
            foreach (var obj in list)
            {
                if (FileChecker.IsExecutable(obj))
                {
                    return [target, obj];
                }
            }
            return null;
        }
    }
}