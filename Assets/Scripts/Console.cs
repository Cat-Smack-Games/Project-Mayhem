using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Console
{
    private static List<string> log = new List<string>();
    private static List<Color> logColor = new List<Color>();

    public static void LogMessage(string msg, Color msgColor)
    {
        log.Add(msg);
        logColor.Add(msgColor);
    }
 

    public static List<string> GetLog()
    {
        return log;
    }
    public static List<Color> GetLogColors()
    {
        return logColor;
    }

    public static void RunCommand(string cmd)
    {
        Console.LogMessage(cmd, Color.white);
        string[] args = cmd.Split(' ');
        if (args[0].Equals("rungm"))
        {
            Game.StartGameMode(args);
        }
        else if (args[0].Equals("leave") || args[0].Equals("disconnect"))
        {
            Game.LeaveGame();
        }
        else if (args[0].Equals("leveleditor") || args[0].Equals("editor"))
        {
            Game.OpenLevelEditor();
        }
        else if (args[0].Equals("debug_registry"))
        {
            Console.LogMessage("Gamemodes Registered: " + ContentManager.GetGamemodes().Count, Color.grey);
            Console.LogMessage("Textures Registered: " + ContentManager.GetTextures().Count, Color.grey);
            Console.LogMessage("Models Registered: " + ContentManager.GetModels().Count, Color.grey);

        }
        else if (args[0].Equals("debug_player"))
        {
            if(Game.LocalPlayer() != null)
            {
                Console.LogMessage("Player ID: " + Game.LocalPlayer().ID, Color.grey);
                Console.LogMessage("Player Username: " + Game.LocalPlayer().Username, Color.grey);
                Console.LogMessage("Player Session Ticket: " + Game.LocalPlayer().SessionTicket, Color.grey);
            }
            else
            {
                Console.LogMessage("No Local Player Found!" + Game.LocalPlayer().ID, Color.red);
            }

        }
        else
        {
            Console.LogMessage("Unknown Command", Color.red);
            return;
        }
    }
}
