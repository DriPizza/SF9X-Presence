using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SF9X
{
    public static class utilityClass
    {
        [DllImport("user32.dll")]
        public static extern nint GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(nint windowHandle, out uint processId);

        public static Process? returnProcess(string processName)
        {
            processName = processName.ToLower();
            foreach (Process v in Process.GetProcesses())
            {
                try
                {
                    string? fileName;
                    fileName = FileVersionInfo.GetVersionInfo(v.MainModule.FileName).OriginalFilename;
                    
                    if (!string.IsNullOrEmpty(fileName) && fileName.ToLower() == processName)
                    {
                        return v;
                    }

                }
                catch (System.ComponentModel.Win32Exception) { continue; }
                catch (System.InvalidOperationException) { continue; }
            }
            return null;
        }
    }
}