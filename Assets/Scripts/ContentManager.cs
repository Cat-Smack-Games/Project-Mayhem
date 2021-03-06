using ProjectMayhemContentFramework.Content;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContentManager
{
   private static Dictionary<string,Gamemode> gamemodes = new Dictionary<string,Gamemode>();
   private static Dictionary<string, Map> maps = new Dictionary<string, Map>();
   private static Dictionary<string, Model> models = new Dictionary<string, Model>();
   private static Dictionary<string, ProjectMayhemContentFramework.Content.Texture > textures = new Dictionary<string,ProjectMayhemContentFramework.Content.Texture >();

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
    public static void AddTexture(string id, ProjectMayhemContentFramework.Content.Texture texture)
    {
        textures.Add(id, texture);
    }
    public static ProjectMayhemContentFramework.Content.Texture GetTexture(string id)
    {
        return textures[id];
    }
    public static Dictionary<string, ProjectMayhemContentFramework.Content.Texture> GetTextures()
    {
        return textures;
    }
    public static Model GetModel(string id)
    {
        return models[id];
    }
    public static Dictionary<string, Model> GetModels()
    {
        return models;
    }
}
