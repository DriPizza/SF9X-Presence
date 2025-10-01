using System.ComponentModel;
using Discord;

namespace SF9X
{
    public class richPresencer
    {
        public Discord.Discord discord = new Discord.Discord(1409157227381784617, (UInt16)Discord.CreateFlags.Default);

        Dictionary<string, object> updateContent = new Dictionary<string, object>()
        {
            ["gameName"] = "",
            ["state"] = "",

            ["smallImage"] = "",
            ["largeImage"] = "",
            ["largeText"] = "",
            ["smallText"] = "",
            ["elapsedTime"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
        string? currentGame = "";
        
        // Updater
        public void updatePresence(SF9X.emulatorController emulatorController, bool startup)
        {
            Dictionary<string, object>? emulatorState = emulatorController.returnEmulatorState();
            var activityManager = discord.GetActivityManager();

            if (currentGame != (string)emulatorState["gameName"])
            {
                currentGame = (string)emulatorState["gameName"];
                updateContent["elapsedTime"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }

            updateContent["gameName"] = emulatorState["gameName"];
            updateContent["state"] = emulatorState["pauseState"];

            updateContent["largeImage"] = "defaulticon"; //
            activityManager.UpdateActivity(newActivity(updateContent),
                result =>
                {
                    if (startup) { Console.WriteLine("Ready!"); }
                });
            discord.RunCallbacks();
        }

        // Methods
        private Discord.Activity newActivity(Dictionary<string, object> updateInfo)
        {
            Discord.Activity updatedActivity = new Discord.Activity
            {
                Details = (string)updateInfo["gameName"] != "" ? "Playing " + updateInfo["gameName"] : "Picking a game",
                State = (string)updateInfo["state"],
                Assets = {
                    SmallImage = (string)updateInfo["smallImage"],
                    LargeImage = (string)updateInfo["largeImage"],
                },
                Timestamps = {
                    Start = (long)updateInfo["elapsedTime"],
                }
                
            };

            return updatedActivity;
        }
    }
}