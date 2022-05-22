using ProjectCoreFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Content c = ProjectCoreFramework.ContentData.LoadContentData();
        Debug.Log(c.gamemodes.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
