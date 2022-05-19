using ProjectCoreFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.StartServer();
        if (NetworkManager.Singleton.IsServer)
        {
            // This will not send any network packets but will log it locally on the server
            NetworkLog.LogInfoServer("Starting " + Project.GameName + "Game Server.");
        }
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<UNetTransport>().ConnectAddress = "";
    }
    
 

  
    // Update is called once per frame
    void Update()
    {
        
    }
}
