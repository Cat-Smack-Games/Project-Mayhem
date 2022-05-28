using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UI 
{
  public static class Popup
    {
        public static void Create(string title, string message, bool yesNo = false, System.Action yesAction = null, System.Action noAction = null)
        {
            GameObject popup = GameObject.Instantiate(Resources.Load("Popup") as GameObject, Vector3.zero, Quaternion.identity);
            popup.GetComponent<PopupPromptUI>().LoadInfo(title, message, yesNo, yesAction, noAction);
        }
    }

}
