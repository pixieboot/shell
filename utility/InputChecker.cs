namespace shell.utility
{
    internal class InputChecker
    {
        public static bool Check(string input)
        {
            string[] substrings = input.Split(" ");
            List<string>? result = FileChecker.CheckIfFileExists(substrings[0]);
            if (result != null)
            {
                foreach (var obj in result)
                {
                    if (!FileChecker.IsExecutable(obj))
                    {
                        return false;
                    }
                    if (substrings.Length != 0)
                    {
                        if (RunProgram.Execute(substrings[0], substrings[1..]))
                        {
                            return true;
                        }
                        else { return false; }
                    }
                    else
                    {
                        if (RunProgram.Execute(substrings[0], null))
                        {
                            return true;
                        }
                        else { return false; }
                    }
                }
                return false;
            }
            else { return false; }
        }
    }
}