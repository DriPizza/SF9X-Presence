using Discord;
using SF9X;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Setup
        Console.Title = "SF9X Presence";

        // Init rich pesence
        SF9X.emulatorController emulatorController = new SF9X.emulatorController();
        SF9X.richPresencer richPresence = new SF9X.richPresencer();

        emulatorController.updateEmulator(true);
        Console.WriteLine("Please wait...");
        richPresence.updatePresence(emulatorController,true);
        
        while (emulatorController.updateEmulator(false) == true)
        {
            richPresence.updatePresence(emulatorController,false);
        }
    }
}
