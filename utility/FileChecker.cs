using System.Runtime.InteropServices;
using System.Text;

namespace shell.utility
{
    internal class FileChecker
    {
        internal static bool IsExecutable(string? filePath)
        {
            try
            {
                if (!File.Exists(filePath) || filePath == null)
                {
                    return false;
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return IsExecutableWin(filePath);

                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return IsExecutableUnix(filePath);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsExecutableWin(string filePath)
        {
            string ext = Path.GetExtension(filePath);
            string[] executableExtension = [".exe", ".bat", ".cmd", ".com", ".ps1"];
            try
            {
                var firstTwoBytes = new byte[2];
                using var fileStream = File.Open(filePath, FileMode.Open);
                fileStream.ReadExactly(firstTwoBytes, 0, 2);
                if (Encoding.UTF8.GetString(firstTwoBytes) == "MZ")
                {
                    return true;
                }
                else if (Array.Exists(executableExtension, i => i == ext))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                if (Array.Exists(executableExtension, i => i == ext))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static bool IsExecutableUnix(string filePath)
        {
            try
            {
                UnixFileMode mode = File.GetUnixFileMode(filePath);
                return (mode & (UnixFileMode.UserExecute | UnixFileMode.GroupExecute | UnixFileMode.OtherExecute)) != 0;
            }
            catch { return false; }
        }

        internal static bool CheckIfAnyFileWithTargetNameIsExecutable(string target)
        {
            string? path = Environment.GetEnvironmentVariable("PATH");
            if (path == null)
            {
                return false;
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
                        if (IsExecutable(filePath))
                        {
                            return true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return false;
        }

        internal static List<string>? CheckIfFileExists(string target)
        {
            List<string> filesWithSameName = [];
            string? path = Environment.GetEnvironmentVariable("PATH");
            if (path == null)
            {
                return null;
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
                        filesWithSameName.Add(filePath);
                    }
                }
            }
            return filesWithSameName;
        }
    }
}
