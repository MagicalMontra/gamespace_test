using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Zenject;

public class GUILoginPanel : MonoBehaviour
{
    [Serializable]
    public class Settings
    {
        public TMP_InputField usernameField;
        public TMP_InputField passwordField;
        public Button loginButton;
        public Button registerButton;
    }

    Settings _settings;

    [Inject]
    public void Constructor(Settings settings)
    {
        _settings = settings;
    }

    public Settings GetSettings()
    {
        return _settings;
    }

    public void OnCharacterSignalFired(CharacterSelectSignal signal)
    {
        gameObject.SetActive(false);
    }

    public void OnLogoutSignalFired(LogoutSignal signal)
    {
        gameObject.SetActive(true);
    }
}
