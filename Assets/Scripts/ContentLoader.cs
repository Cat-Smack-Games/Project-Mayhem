using Microsoft.CSharp;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.SharedModels;
using ProjectCoreFramework;
using ProjectMayhemContentFramework.Content;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContentLoader : MonoBehaviour
{
    private static string ContentDir = "Assets/Content/";
    private static string Gamemode = "Gamemodes/";
    private static string Map = "Maps/";
    private static List<Action> actions = new List<Action>();
    private static GameObject loadInfo;
    private static int taskCount = 0;
    private static int taskComplete = 0;
    private static float coreContent;
    private static float communityContent;
    private static bool isLoading = true;
    // Start is called before the first frame update
    void Start()
    {
        Console.LogMessage("Game Started", Color.yellow);
        // If you want a synchronous result, you can call loginTask.Wait() - Note, this will halt the program until the function returns
        if (Game.LocalPlayer().SessionTicket == null)
        {
            SceneManager.LoadScene("auth");
        }
        actions.Add(LoadCoreContent);
        actions.Add(LoadCommunityContent);
        loadInfo = GameObject.FindWithTag("LoadingInfo");
        Content content = ProjectCoreFramework.ContentData.LoadContentData();
        foreach (KeyValuePair<string, Gamemode> pair in content.gamemodes)
        {
            ContentManager.AddGamemode(pair.Key, pair.Value);
        }
        foreach (KeyValuePair<string, ProjectMayhemContentFramework.Content.Texture> pair in content.textures)
        {
            ContentManager.AddTexture(pair.Key, pair.Value);
        }
        foreach (KeyValuePair<string, Model> pair in content.models)
        {
            ContentManager.AddModel(pair.Key, pair.Value);
        }

        Console.LogMessage("CCC : " + ContentManager.GetTextures().Count.ToString(), Color.yellow);
        Load();
        
    }
    void Update()
    {
        if(taskCount > 0)
        {
            coreContent = (taskComplete / taskCount) / 1;
        }
     
        UpdatePercent();
        if(!isLoading)
        {

            GameObject.FindGameObjectWithTag("MainMenu").transform.GetChild(0).gameObject.SetActive(true);   
           // GameObject.FindGameObjectWithTag("LoadingScreen").SetActive(false);
        }
    }
  
   
    public static async Task Load()
    {
        await Task.Run(() =>
        {
           

        });
        isLoading = false;
        
    }

    private float CurrentPercent()
    {
        return (coreContent) ;
    }
    public void UpdatePercent()
    {
       // loadInfo.GetComponent<LoadingBar>().percent = CurrentPercent();
    }
    public static void LoadText(string s)
    {
        loadInfo.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = s;
    }
    public void LoadCoreContent()
    {

    }
    public void LoadCommunityContent()
    {
        string[] dirs = Directory.GetDirectories(ContentDir + Gamemode, "*", SearchOption.TopDirectoryOnly);
        Debug.Log("Gamemodes found: " + dirs.Length);
        foreach (string dir in dirs)
        {
            Debug.Log(dir);
            LoadGamemode(dir);
        }

        string[] dirs2 = Directory.GetDirectories(ContentDir + Map, "*." + Project.MapExtention, SearchOption.TopDirectoryOnly);
        Debug.Log("Maps found: " + dirs.Length);
        foreach (string dir in dirs2)
        {
            Debug.Log(dir);
            LoadMap(dir);
        }
    }
    public bool LoadMap(string mapPath)
    {
        Map map = new Map(mapPath);
        return false;
    }
    public bool LoadGamemode(string gamemodePath)
    {
        Debug.Log(gamemodePath);
        string[] fileEntries = Directory.GetFiles(gamemodePath);
        string baseScript = "";
        string baseAssembly = "";
        GamemodeInfo gamemodeInfo = null;
        foreach (string fileName in fileEntries)
        {
            if(fileName.Contains("gamemode.json") && !fileName.Contains(".meta")) {
                string json = File.ReadAllText(fileName);
                Debug.Log(json);
                gamemodeInfo = JsonConvert.DeserializeObject<GamemodeInfo>(json);
                baseAssembly = gamemodeInfo.core.Split('\\')[gamemodeInfo.core.Split('\\').Length-1].Split('.')[0];
            }
        }
       
        if(gamemodeInfo != null)
        {
            baseScript = File.ReadAllText(gamemodePath + "/" + gamemodeInfo.core);
        }
        if(!baseScript.Equals(""))
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
            ContentManager.AddGamemode(result.GamemodeInfo().gamemode_id, result);
            //main.Invoke(null, null);
        }
       
        Debug.Log("Gamemodes: " + ContentManager.GetGamemodes().Count);
        Debug.Log("Gamemode: " + ContentManager.GetGamemode("test-gamemode").GamemodeInfo().gamemode_name);
        return false;
    }
 
}
