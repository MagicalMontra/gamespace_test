using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoginController : MonoBehaviour
{
    PlayfabLogin _loginManager;
    PlayfabCharacter _characterCreator;
    SignalBus _signalBus;

    [Inject]
    public void Contructor(PlayfabLogin loginManager, PlayfabCharacter characterCreator, SignalBus signalBus)
    {
        _loginManager = loginManager;
        _characterCreator = characterCreator;
        _signalBus = signalBus;
    }

    public void Login(string username, string password)
    {
        _loginManager.Login(username, password);
    }

    public void Register(string username, string password)
    {
        _loginManager.Register(username, password);
    }
}
