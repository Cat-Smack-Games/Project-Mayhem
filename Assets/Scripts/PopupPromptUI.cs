using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPromptUI : MonoBehaviour
{
    private GameObject acceptButton;
    private GameObject affirmButton;
    private GameObject cancelButton;

    private TMPro.TMP_Text title;
    private TMPro.TMP_Text description;
    // Start is called before the first frame update
    void Start()
    {
        acceptButton = this.gameObject.transform.GetChild(0).Find("Confirm").gameObject;
        affirmButton = this.gameObject.transform.GetChild(0).Find("Okay").gameObject;
        cancelButton = this.gameObject.transform.GetChild(0).Find("Deny").gameObject;
        title = this.gameObject.transform.GetChild(0).Find("Title").gameObject.GetComponent<TMPro.TMP_Text>();
        description = this.gameObject.transform.GetChild(0).Find("Desc").gameObject.GetComponent<TMPro.TMP_Text>();
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
            affirmButton.GetComponent<Button>().onClick.AddListener(delegate { yesAction(); });
            cancelButton.GetComponent<Button>().onClick.AddListener(delegate { noAction(); });
        }
        else
        {
            acceptButton.SetActive(false);
            affirmButton.SetActive(true);
            cancelButton.SetActive(false);
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
