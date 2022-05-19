using ProjectFramework.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Gamemode gamemode;
    // Start is called before the first frame update
    void Start()
    {
       gamemode = Game.LoadGameMode();
        if(gamemode == null)
        {
            //SceneManager.LoadScene("mainmenu");
        }
        NetworkManager.Singleton.StartClient();
      
        if (NetworkManager.Singleton.IsClient)
        {
            // This will not send any network packets but will log it locally on the server
            NetworkLog.LogInfoServer("Player Joined Server");

        }
       // gamemode.Init();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
