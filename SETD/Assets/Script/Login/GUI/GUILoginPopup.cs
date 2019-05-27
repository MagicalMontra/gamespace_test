using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using Zenject;
using System;
using UnityEngine.UI;
using TMPro;

public class GUILoginPopup : MonoBehaviour
{
    [Serializable]
    public class Settings
    {
        public Image bgTarget;
        public Button okayButton;
        public TextMeshProUGUI header;
        public TextMeshProUGUI content;
    }
    Settings _settings;
    SignalBus _signalBus;

    [Inject]
    public void Constructor(Settings settings, SignalBus signalBus)
    {
        _settings = settings;
        _signalBus = signalBus;
        _settings.okayButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    public void SignalReceiver(LoginSignal signal)
    {
        if (signal._isLoggedin)
        {
            _signalBus.Fire(new CharacterSelectSignal());
            return;
        }

        PopContent("Login Error", signal.GetContent());
        gameObject.SetActive(true);
    }

    public void SignalReceiver(RegisterSignal signal)
    {
        if (signal._isValid)
        {
            _signalBus.Fire(new CharacterSelectSignal());
            return;
        }

        PopContent("Register Error", signal.GetContent());
    }

    void PopContent(string header, string content)
    {
        _settings.header.text = header;
        _settings.content.text = content;
        gameObject.SetActive(true);
    }
}
