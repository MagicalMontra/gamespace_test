using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GUICharacterSelection : MonoBehaviour
{
    [Serializable]
    public class Settings
    {
        public Button startButton;
        public Button logoutButton;
        public Button[] createButtons;
        public GUICharacterCreate createPanel;
        public GUILoadingScreen dataLoading;
    }
    Settings _settings;
    CharacterServiceController _controller;

    [Inject]
    public void Constructor(Settings settings)
    {
        _settings = settings;
    }

    void OnCreateButtonClicked()
    {
        _settings.createPanel.gameObject.SetActive(true);
    }

    public void OnCharacterSelectSignalFired(CharacterSelectSignal signal)
    {
        gameObject.SetActive(true);
    }

    public void OnLogoutSignalFired(LogoutSignal signal)
    {
        gameObject.SetActive(false);
    }

    public void OnLoadingSignalFired(LoadingSignal signal)
    {
        _settings.dataLoading.StartLoading();
    }

    public void OnFinishLoadingSignalFired(FinishLoading signal)
    {
        _settings.dataLoading.EndLoading();

        for (int i = 0; i < _settings.createButtons.Length; i++)
        {
            _settings.createButtons[i].onClick.AddListener(OnCreateButtonClicked);
        }

        _settings.createPanel.SetupRace();
        _settings.createPanel.SetupClass();
    }

    IEnumerator DelayResolver()
    {
        yield return new WaitForSecondsRealtime(0.5f);

    }
}
