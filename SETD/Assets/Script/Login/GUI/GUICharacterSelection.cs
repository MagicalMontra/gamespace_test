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
    public void Constructor(Settings settings, CharacterServiceController controller)
    {
        _settings = settings;
        _controller = controller;

        foreach (var button in _settings.createButtons)
        {
            button.onClick.AddListener(OnCreateButtonClicked);
        }
    }

    void OnCreateButtonClicked()
    {
        _settings.createPanel.SetupRace(_controller.GetRaces());
        _settings.createPanel.SetupClass(_controller.GetClasses());
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
    }
}
