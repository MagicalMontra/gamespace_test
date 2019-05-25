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
        _settings.okayButton.onClick.AddListener(() => PopupSetActive(false));
    }

    public void Pop(LoginSignal signal)
    {
        if (signal._isLoggedin)
        {
            _signalBus.Fire(new CharacterSelectSignal());
            return;
        }

        _settings.header.text = "Login Error";
        _settings.content.text = signal.GetContent();
        gameObject.SetActive(true);
    }

    public void PopupSetActive(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}
