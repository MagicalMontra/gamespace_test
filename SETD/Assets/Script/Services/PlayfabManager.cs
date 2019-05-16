using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    #region  API

    #region  Connection Resolver

    // IEnumerator ConnectionResolver(Action api)
    // {
    //     api();
    // }

    #endregion

    // async Task LoginTask(object task)
    // {
    //     await task();
    // }
    void PlayfabRegister(string username, string password)
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Username = username,
            Password = password,
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterCallback, ErrorCallback);
    }

    void PlayfabLogin(string username, string password)
    {
        var loginRequest = new LoginWithPlayFabRequest
        {
            Username = username,
            Password = password
        };

        PlayFabClientAPI.LoginWithPlayFab(loginRequest, LoginCallback, ErrorCallback);
    }

    #endregion

    #region  Callback

    void RegisterCallback(RegisterPlayFabUserResult result)
    {

    }

    void LoginCallback(LoginResult result)
    {

    }

    void ErrorCallback(PlayFabError error)
    {

    }

    #endregion
}
