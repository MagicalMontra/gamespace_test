using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayfabServices.User;
using UnityEngine;
using Zenject;


public class LoginController : MonoBehaviour
{
    SignalBus _signalBus;

    [Inject]
    public void Contructor(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Login(string username, string password)
    {
        PlayfabLogin.Login(username, password, OnLoggedIn, OnLoginError);
    }

    public void Register(string username, string password)
    {
        PlayfabLogin.Register(username, password, OnRegisterComplete, OnRegisterError);
    }


    #region  Signal
    void OnRegisterComplete(RegisterPlayFabUserResult result)
    {
        _signalBus.TryFire(new RegisterSignal(true, "Register Complete"));
    }
    void OnRegisterError(PlayFabError error)
    {
        _signalBus.TryFire(new RegisterSignal(false, error.ErrorMessage)); ;
    }

    void OnLoggedIn(LoginResult result)
    {
        _signalBus.TryFire(new LoginSignal(PlayFabClientAPI.IsClientLoggedIn(), ""));
    }

    void OnLoginError(PlayFabError error)
    {
        _signalBus.TryFire(new LoginSignal(PlayFabClientAPI.IsClientLoggedIn(), error.ErrorMessage));
    }
    #endregion
}
