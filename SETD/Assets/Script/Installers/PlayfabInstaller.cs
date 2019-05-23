using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using System;

public class PlayfabInstaller : MonoInstaller<PlayfabInstaller>
{
    [SerializeField]
    private LoginGUIAsset _loginAsset;

    public override void InstallBindings()
    {
        Container.Bind<PlayfabService>().AsSingle();
        Container.Bind<GUILoginHandler>().FromComponentInHierarchy().AsSingle().WithArguments(Container.Bind<PlayfabManager>().FromComponentInHierarchy().AsSingle(), _loginAsset);
    }
}

[Serializable]
public class LoginGUIAsset
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public Button loginButton;
    public Button registerButton;
}
