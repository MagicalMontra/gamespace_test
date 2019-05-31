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
        public GUICharacterSlot[] cSlots;
    }
    Settings _settings;
    CharacterServiceController _controller;

    [Inject]
    public void Constructor(Settings settings, CharacterServiceController controller)
    {
        _settings = settings;
        _controller = controller;
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

    public void OnRefreshedCharacterFired(RefreshCharacterListSignal signal)
    {
        for (int i = 0; i < _settings.cSlots.Length; i++)
        {
            if (i >= signal._characters.Count)
                _settings.cSlots[i].RemoveSlot();
            else
            {
                _settings.cSlots[i].SetupSlot(signal._characters[i]);
                _settings.cSlots[i].SetRemoveButton(_controller);
            }
        }
    }

    public void OnCharacterCreatedSignalFired(CharacterCreatedSignal signal)
    {
        _settings.createPanel.OnCharacterCreated();
    }
}
