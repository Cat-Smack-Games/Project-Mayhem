using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPromptUI : MonoBehaviour
{
    public GameObject acceptButton;
    public GameObject affirmButton;
    public GameObject cancelButton;

    public TMPro.TMP_Text title;
    public TMPro.TMP_Text description;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void LoadInfo(string title, string message, bool yesNo, System.Action yesAction, System.Action noAction)
    {
        this.title.text = title;
        this.description.text = message;
        if (yesNo)
        {
            acceptButton.SetActive(true);
            affirmButton.SetActive(false);
            cancelButton.SetActive(true);
            acceptButton.GetComponent<Button>().onClick.AddListener(delegate { yesAction(); });
            acceptButton.GetComponent<Button>().onClick.AddListener(delegate { Close(); });

            if (noAction != null)
            {
               cancelButton.GetComponent<Button>().onClick.AddListener(delegate { noAction(); });
            }
               cancelButton.GetComponent<Button>().onClick.AddListener(delegate { Close(); });
        }
        else
        {
            acceptButton.SetActive(false);
            affirmButton.SetActive(true);
            cancelButton.SetActive(false);
        }
    }
    public void Close()
    {
        GameObject.Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
