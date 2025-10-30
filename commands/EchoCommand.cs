namespace src.commands
{
    public static class EchoCommand
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