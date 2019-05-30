using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Zenject;
using System;

public class GUILoginController : MonoBehaviour
{

    [Serializable]
    public class Settings
    {
        public GUILoginPanel loginPanel;
        public GUILoginPopup popup;
        //public GUILoadingScreen loginLoading;
    }
    Settings _settings;
    LoginController _controller;

    [Inject]
    public void Constructor(Settings settings, LoginController controller)
    {
        _settings = settings;
        _controller = controller;
    }

    void Start()
    {
        _settings.loginPanel.GetSettings().loginButton.onClick.AddListener(OnLoginButtonClicked);
        _settings.loginPanel.GetSettings().registerButton.onClick.AddListener(OnRegisterButtonClicked);
    }

    public void OnLoginSignalFired(LoginSignal signal)
    {
        _settings.popup.OnLoginSignalFired(signal);
    }
    public void OnRegisterSignalFired(RegisterSignal signal)
    {
        _settings.popup.OnRegisterSignalFired(signal);
    }
    public void OnLogoutSignalFired(LogoutSignal signal)
    {
        _settings.loginPanel.OnLogoutSignalFired(signal);
    }

    public void OnErrorSignalFired(ErrorSignal signal)
    {
        _settings.popup.OnErrorSignalFire(signal);
    }

    public void OnLoginButtonClicked()
    {
        var settings = _settings.loginPanel.GetSettings();
        _controller.Login(settings.usernameField.text, settings.passwordField.text);
    }

    public void OnRegisterButtonClicked()
    {
        var settings = _settings.loginPanel.GetSettings();
        _controller.Register(settings.usernameField.text, settings.passwordField.text);
    }
}
