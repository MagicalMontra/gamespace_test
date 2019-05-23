using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Zenject;

public class LoginManager : PlayfabService
{
    #region API

    internal void Login(string username, string password)
    {
        var loginRequest = new LoginWithPlayFabRequest
        {
            Username = username,
            Password = password
        };

        PlayFabClientAPI.LoginWithPlayFab(loginRequest, OnLoginComplete, OnError);
    }
    internal void Register(string username, string password)
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            TitleId = PlayFabSettings.TitleId,
            Username = username,
            Password = password,
            RequireBothUsernameAndEmail = false,
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterComplete, OnError);
    }

    #endregion

    #region OnResponse

    void OnRegisterComplete(RegisterPlayFabUserResult result)
    {
        // Signal UI Changes here
#if UNITY_EDITOR
        Debug.Log(result.PlayFabId + " account has been created.");
#endif
    }

    void OnLoginComplete(LoginResult result)
    {
        // Signal UI Changes here
#if UNITY_EDITOR
        Debug.Log(result.PlayFabId + " logged in.");
#endif
    }

    #endregion
}
