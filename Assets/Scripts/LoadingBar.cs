using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public GameObject loadingBar;
    // Start is called before the first frame update
    public float percent;



    public void Update()
    {
        if(percent > 1)
        {
            percent = 1;
        }
        loadingBar.GetComponent<RectTransform>().sizeDelta = new Vector2 ( (float)(1423.56 * percent), 10.5828f);
    }
}
