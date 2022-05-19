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
        else
        {
            Console.LogMessage("Unknown Command", Color.red);
            return;
        }
    }
}
