using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Zenject;

public class GUILoginHandler : MonoBehaviour
{
    TMP_InputField _usernameField;
    TMP_InputField _passwordField;
    Button _loginButton;
    Button _registerButton;
    LoginManager _loginManager;

    [Inject]
    public void Constuctor(PlayfabManager playfabManager, LoginGUIAsset asset)
    {
        _loginManager = playfabManager.GetService() as LoginManager;
        _usernameField = asset.usernameField;
        _passwordField = asset.passwordField;
        asset.loginButton.onClick.AddListener(OnLoginButtonClicked);
        asset.registerButton.onClick.AddListener(OnRegisterButtonClicked);
    }

    public void OnLoginButtonClicked()
    {
        _loginManager.Login(_usernameField.text, _passwordField.text);
    }

    public void OnRegisterButtonClicked()
    {
        _loginManager.Register(_usernameField.text, _passwordField.text);
    }
}
