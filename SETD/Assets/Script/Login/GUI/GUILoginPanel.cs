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
    SignalBus _signalBus;

    [Inject]
    public void Constructor(Settings settings, SignalBus signalBus)
    {
        _settings = settings;
        _signalBus = signalBus;

        SignalReceiver();
    }

    public Settings GetSettings()
    {
        return _settings;
    }

    void SignalReceiver()
    {
        // In case the same overload need other signal

        _signalBus.Subscribe<CharacterSelectSignal>(Disabled);
    }

    void Disabled(CharacterSelectSignal signal)
    {
        gameObject.SetActive(false);
    }
}
