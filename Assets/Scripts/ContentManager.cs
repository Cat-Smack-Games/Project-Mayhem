using ProjectFramework.Content;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContentManager
{
   private static Dictionary<string,Gamemode> gamemodes = new Dictionary<string,Gamemode>();
   private static Dictionary<string, Map> maps = new Dictionary<string, Map>();
   private static Dictionary<string, Model> models = new Dictionary<string, Model>();
   private static Dictionary<string, ProjectFramework.Content.Texture> textures = new Dictionary<string, ProjectFramework.Content.Texture>();

    public static void AddGamemode(string id, Gamemode gamemode)
    {
        gamemodes.Add(id, gamemode);
    }
    public static Gamemode GetGamemode(string id)
    {
        return gamemodes[id];
    }
    public static Dictionary<string, Gamemode> GetGamemodes()
    {
        return gamemodes;
    }

    public static void AddMap(string id, Map map)
    {
        maps.Add(id, map);
    }
    public static void AddModel(string id, Model model)
    {
        models.Add(id, model);
    }
    public static void AddTexture(string id, ProjectFramework.Content.Texture texture)
    {
        textures.Add(id, texture);
    }
}
