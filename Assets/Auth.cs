using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Auth : MonoBehaviour
{
    public TMPro.TMP_InputField username;
    public TMPro.TMP_InputField pass;
    private string sessionTicket = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        Debug.Log(username.text);
        Debug.Log(pass.text);
        var request = new LoginWithPlayFabRequest { Username = username.text, Password = pass.text, TitleId = "8B5F1" };
        PlayFabClientAPI.LoginWithPlayFab(request, OnSuccessfulLogin, OnLoginFailure);
    }

    public void OnSuccessfulLogin(LoginResult result)
    {
        sessionTicket = result.SessionTicket;
        var request = new GetAccountInfoRequest { PlayFabId = result.PlayFabId };
        PlayFabClientAPI.GetAccountInfo(request, GetInfo, OnLoginFailure);
       
       
    }
    public void GetInfo(GetAccountInfoResult result)
    {
        Game.SetLocalPlayer(new ProjectMayhemContentFramework.Player()
        {
            ID = result.AccountInfo.PlayFabId,
            Username = result.AccountInfo.Username,
            SessionTicket = sessionTicket,
            AvatarImage = ""
        });
        SceneManager.LoadScene("mainmenu");
    }
    public void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Bad Login");
    }
}
