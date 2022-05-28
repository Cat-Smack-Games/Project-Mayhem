using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuUI : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }
    public void CloseMenu()
    {
        menu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && menu.activeSelf)
        {
            CloseMenu();
        }
    }
}
