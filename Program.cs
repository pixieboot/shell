using static System.StringComparison;

namespace shell { 
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("$ ");
                String? input = Console.ReadLine()?.Trim();
    
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("");
                }
    
                else if (input.StartsWith("type ", OrdinalIgnoreCase))
                {
                    Type.BuiltInValidator(input);
                }
    
                else if (input.StartsWith("echo ", OrdinalIgnoreCase) || input.Equals("echo", OrdinalIgnoreCase))
                {
                    Echo.Reply(input);
                }
    
                else if (input.StartsWith("exit", OrdinalIgnoreCase))
                {
                    Exit.ExitCode(input);
                }
    
                else
                {
                    Console.WriteLine($"{input}: command not found");
                }
            }
        }
    }
    
    public static class Exit
    {
        public static void ExitCode(string input)
        {
            if (input.Equals("exit 1", OrdinalIgnoreCase))
            {
                Environment.Exit(1);
            }
    
            if (input.Equals("exit", OrdinalIgnoreCase) || (input.Equals("exit 0", OrdinalIgnoreCase)))
            {
                Environment.Exit(0);
            }
        }
    }
    
    public static class Type
    {
        private static string[] types = { "echo", "exit", "type" };
    
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
    
        public static void BuiltInValidator(string input)
        {
            string substring = input.Substring(5);
            int result = FindElement(types, substring);
    
            if (result == 0)
            {
                Console.WriteLine($"{substring} is a shell builtin");
            }
            if (result == -1)
            {
                Console.WriteLine($"{substring} not found");
            }
        }
    }
    
    public static class Echo
    {
        public static void Reply(string input)
        {
            if (input.Equals("echo"))
            {
                Console.WriteLine("ECHO is on.");
            }
            else
            {
                Console.WriteLine($"{input.Substring(5)}");
            }
        }
    }
}