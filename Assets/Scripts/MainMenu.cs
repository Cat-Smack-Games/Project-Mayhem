using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Dictionary<string, GameObject> menus = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitMenuDatabase();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("TMP_Username");
        for(int i = 0; i < objs.Length; i++)
        {
            objs[i].GetComponent<TMPro.TMP_Text>().text = Game.LocalPlayer().Username;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenWindow(string window)
    {
        menus[window].SetActive(true);
    }
    private void InitMenuDatabase()
    {
        menus.Add("play", this.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject);
        menus.Add("play_games", menus["play"].transform.GetChild(4).transform.GetChild(0).gameObject);
    }
}
