using static System.StringComparison;

namespace shell.commands
{
    internal static class ExitCommand
    {
        public static void ExitCode(string input)
        {
            if (input.Equals("exit 1", OrdinalIgnoreCase))
            {
                Environment.Exit(1);
            }
            else if (input.Equals("exit", OrdinalIgnoreCase) || input.Equals("exit 0", OrdinalIgnoreCase))
            {
                Environment.Exit(0);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}