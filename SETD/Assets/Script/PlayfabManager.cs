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

    async void ConnectionResolver()
    { 

    }

    #endregion

    // async Task LoginTask(object task)
    // {
    //     await task();
    // }

    void LoginWithCustomID()
    {
        var loginRequest = new LoginWithCustomIDRequest
        {
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(loginRequest, LoginCallback, ErrorCallback);
    }

    #endregion

    #region  Callback

    void LoginCallback(LoginResult result)
    { 

    }

    void ErrorCallback(PlayFabError error)
    { 

    }

    #endregion
}
