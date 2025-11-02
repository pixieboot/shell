using System.Diagnostics;

namespace shell.utility
{
    internal class RunProgram
    {
        internal static bool Execute(string target, string[]? args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(target);

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = target;

            if (args != null)
            {
                string argsString = "";
                for (int i = 0; i < args.Length; i++)
                {
                    argsString += " " + args[i];
                }
                startInfo.Arguments = argsString;
            }
            try
            {
                using var currentProcess = Process.Start(startInfo);
                if (currentProcess == null)
                {
                    Console.WriteLine($"Process returned null: {currentProcess}");
                    return false;
                }
                currentProcess.WaitForExit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to run: {ex}");
                return false;
            }
        }
    }
}
