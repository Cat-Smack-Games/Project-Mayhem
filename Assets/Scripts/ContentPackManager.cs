using Microsoft.CSharp;
using Newtonsoft.Json;
using ProjectMayhemContentFramework.Content;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using static ProjectMayhemContentFramework.Content.Texture;

public class ContentPackManager : Editor
{
   
  
    [MenuItem("Project Mayhem/BuildPack", false, -1)]
    public static void BuildPack()
    {
        string contentPath = Application.dataPath + "/Content/";
        var gamemodeDirs = Directory.GetDirectories(contentPath + "Gamemodes/");
        var textures = Directory.GetFiles(contentPath + "Textures/");
        ProjectCoreFramework.ContentData.Data = new ProjectCoreFramework.Content();
        ProjectCoreFramework.ContentData.Data.gamemodes = new Dictionary<string, Gamemode>();
        ProjectCoreFramework.ContentData.Data.textures = new Dictionary<string, ProjectMayhemContentFramework.Content.Texture>();
        ProjectCoreFramework.ContentData.Data.models = new Dictionary<string, Model>();
        foreach (var dir in gamemodeDirs)
        {
           // LoadGamemode(dir);
            
        }
        ProjectCoreFramework.ContentData.Data.gamemodes.Add(TestGamemode.Main().GamemodeInfo().gamemode_id, TestGamemode.Main());
        foreach (var textPath in textures)
        {
            Debug.Log(textPath);
            if(textPath.EndsWith(".png") || textPath.EndsWith(".jpg"))
            {
                LoadTextures(textPath);
            }
           
            
        }
        ProjectCoreFramework.ContentData.SaveContentData();
    }
    public static void LoadTextures(string texturePath)
    {
        string jsonPath = "";
        if (texturePath.EndsWith(".png"))
        {
            jsonPath = texturePath.Replace(".png", ".json");
        }
        else if(texturePath.EndsWith(".jpg"))
        {
            jsonPath = texturePath.Replace(".jpg", ".json");
        }
            
        ProjectMayhemContentFramework.Content.Texture tempText = new ProjectMayhemContentFramework.Content.Texture();
        tempText.texture = System.Drawing.Image.FromFile(texturePath);
        string json = File.ReadAllText(jsonPath);
        TextureInfo info = JsonConvert.DeserializeObject<TextureInfo>(json);
        tempText.Info = info;
        ProjectCoreFramework.ContentData.Data.textures.Add(info.texture_id,tempText);
    }

    public static void LoadGamemode(string gamemodePath)
    {
        Debug.Log(gamemodePath);
        string[] fileEntries = Directory.GetFiles(gamemodePath);
        string baseScript = "";
        string baseAssembly = "";
        GamemodeInfo gamemodeInfo = null;
        foreach (string fileName in fileEntries)
        {
            if (fileName.Contains("gamemode.json") && !fileName.Contains(".meta"))
            {
                string json = File.ReadAllText(fileName);
                Debug.Log(json);
                gamemodeInfo = JsonConvert.DeserializeObject<GamemodeInfo>(json);
                baseAssembly = gamemodeInfo.core.Split('\\')[gamemodeInfo.core.Split('\\').Length - 1].Split('.')[0];
            }
        }

        if (gamemodeInfo != null)
        {
            baseScript = File.ReadAllText(gamemodePath + "/" + gamemodeInfo.core);
        }
        if (!baseScript.Equals(""))
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;
            parameters.ReferencedAssemblies.Add("ProjectFramework.dll");
            // True - exe file generation, false - dll file generation
            parameters.GenerateExecutable = false;
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, baseScript);
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                throw new InvalidOperationException(sb.ToString());
            }
            Assembly assembly = results.CompiledAssembly;
            Type program = assembly.GetType(baseAssembly);
            Debug.Log(baseAssembly);
            MethodInfo main = program.GetMethod("Main");
            Type binaryFunction = results.CompiledAssembly.GetType(baseAssembly);
            MethodInfo method = binaryFunction.GetMethod("Main");
            Gamemode result = (Gamemode)method.Invoke(null, new object[] { });
            result.AttachGamemodeInfo(gamemodeInfo);
            ProjectCoreFramework.ContentData.Data.gamemodes.Add(result.GamemodeInfo().gamemode_id, result);
            //main.Invoke(null, null);
        }

    
     
    }
}
