using System.Diagnostics;
using System.IO;

namespace SF9X
{
    public class emulatorController
    {
        public Process? emulatorProcess;
        string thisGame = "";

        public bool updateEmulator(bool yield)
        {
            emulatorProcess = utilityClass.returnProcess("snes9x.exe");

            if (emulatorProcess == null)
            {
                if (!yield) { return false; }
                Console.WriteLine("Please, open your Snes9x.");
            }
            while (emulatorProcess == null)
            {
                emulatorProcess = utilityClass.returnProcess("snes9x.exe");
            }

            return true;
        }

        public Dictionary<string, object>? returnEmulatorState()
        {
            // Setup game's name
            string processName = emulatorProcess.MainWindowTitle.Split(" - Snes9x")[0];
            thisGame = processName == "" ? thisGame : (processName == emulatorProcess.MainWindowTitle ? "" : processName);

            // Setup pause state
            bool isPaused = !isEmulatorForeground();
            // transform snes9x.conf later to modify  info on this.

            Dictionary<string, object> emulatorState = new Dictionary<string, object>()
            {
                ["gameName"] = thisGame,
                ["pauseState"] = thisGame == "" ? "" : (isPaused == true ? "Inactive" : "Running") 
            };

            return emulatorState;
        }

        private bool isEmulatorForeground()
        {
            nint foregroundWindow = SF9X.utilityClass.GetForegroundWindow();
            if (foregroundWindow == IntPtr.Zero || emulatorProcess == null){ return false; }

            uint rawProcessId;
            SF9X.utilityClass.GetWindowThreadProcessId(foregroundWindow, out rawProcessId);

            if ((int)rawProcessId != emulatorProcess.Id) { return false; }

            return true;
        }

    }
}