using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleHUD : MonoBehaviour
{

    private Canvas hud;
    private bool hudToggle;
    private GameObject screen;
    private TMPro.TMP_InputField input;
    private bool isInputSelected;

    private int linesRendered;
    // Start is called before the first frame update
    void Start()
    {
        hud = GetComponent<Canvas>();
        screen = this.gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
        input = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (hudToggle)
            {
                hudToggle = false;
                hud.enabled = true;
                input.Select();
                input.ActivateInputField();
            }
            else { 
                hudToggle = true;
                hud.enabled = false;
                isInputSelected = false;
            }
        }
        List<string> lines = Console.GetLog();
        List<Color> lineColors = Console.GetLogColors();
        for (int i = linesRendered; i < lines.Count || (i < 20 && lines.Count > 20); i++)
        {
            Debug.Log(i);
            GameObject line = Instantiate(Resources.Load("console line") as GameObject);
            line.transform.SetParent(screen.transform, false);
            line.GetComponent<TMPro.TMP_Text>().text = lines[i];
            line.GetComponent<TMPro.TMP_Text>().color = lineColors[i];
            linesRendered = i+1;
        }
        if (isInputSelected && (Input.GetKeyDown(KeyCode.KeypadEnter) ||Input.GetKeyDown(KeyCode.Return))) {
            
            Console.RunCommand(input.text);
            input.text = "";
            input.Select();
            input.ActivateInputField();
        }
      
    }

    public void OnSelect()
    {
        isInputSelected = true;
    }
    public void OnDeselect()
    {
        isInputSelected = false;
    }
}
