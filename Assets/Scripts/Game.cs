using ProjectFramework;
using ProjectFramework.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Game
{
    private static Player localPlayer = new Player()
    {
        ID = null,
        SessionTicket = null,
        AvatarImage = null,
        Username = null,
    };
    private static Gamemode gamemode;

    public static void SetLocalPlayer(Player ply)
    {
        localPlayer = ply;
    }
    public static Player LocalPlayer()
    {
        return localPlayer;
    }
    public static void StartGameMode(params string[] args)
    {
        if (args.Length <= 3)
        {
            Console.LogMessage("Cannot start", Color.red);
            return;
        }
        if (args[2].Equals("map") && args[3] != null)
        {

        }
        if (ContentManager.GetGamemodes().ContainsKey(args[1]))
        {
            gamemode = ContentManager.GetGamemodes()[args[1]];
            SceneManager.LoadScene("gamemode");
        }
        else
        {
            Console.LogMessage("Cannot start: unknown gamemode " + args[1], Color.red);
            return;
        }
    }
    public static Gamemode LoadGameMode()
    {
        if(gamemode == null)
        {
            Console.LogMessage("Game started without configuration", Color.red);
            return null;
        }
        else
        {
            Console.LogMessage("Game loaded: " + gamemode.GamemodeInfo().gamemode_name, Color.cyan);
            return gamemode;
        }
    }

    public static void LoadMap(string map)
    {

    }
    public static void LeaveGame()
    {
        SceneManager.LoadScene("mainmenu");
    }

    public class CoreContent
    {
        public static Gamemode[] Gamemodes = new Gamemode[] { new TestGamemode(), new GamemodeTest()};
        public static Map[] Maps = new Map[] {  };
        public static Model[] Models = new Model[] {};
        public static ProjectFramework.Content.Texture[] Textures = new ProjectFramework.Content.Texture[] { };

    }
}
