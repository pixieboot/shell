namespace shell.commands
{
    internal static class EchoCommand
    {
        public static void Reply(string input)
        {
            if (input.Equals("echo"))
            {
                Console.WriteLine("ECHO is on.");
            }
            else
            {
                Console.WriteLine($"{input[5..]}");
            }
        }
    }
}