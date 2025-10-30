using src.commands;
using static System.StringComparison;

namespace shell
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("$ ");
                string? input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.Write("");
                }
                else if (input.StartsWith("type ", OrdinalIgnoreCase))
                {
                    TypeCommand.Validator(input);
                }
                else if (input.StartsWith("echo ", OrdinalIgnoreCase) || input.Equals("echo", OrdinalIgnoreCase))
                {
                    EchoCommand.Reply(input);
                }
                else if (input.StartsWith("exit", OrdinalIgnoreCase))
                {
                    ExitCommand.ExitCode(input);
                }
                else
                {
                    Console.WriteLine($"{input}: command not found");
                }
            }
        }
    }
}