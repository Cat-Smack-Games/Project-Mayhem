using ProjectMayhemContentFramework.Content;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemodeList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(KeyValuePair<string,Gamemode> gamemode in ContentManager.GetGamemodes())
        {
            GameObject panel = Instantiate(Resources.Load("gamemode_info_panel") as GameObject);
            panel.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = gamemode.Value.GamemodeInfo().gamemode_name;
            panel.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = gamemode.Value.GamemodeInfo().gamemode_desc;
            panel.transform.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
