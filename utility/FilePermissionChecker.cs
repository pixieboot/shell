using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace src.utility
{
    internal class FilePermissionChecker
    {
        internal static bool IsExecutable(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
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
            return Array.Exists(executableExtension, i => i == ext);
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
    }
}
