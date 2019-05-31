using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class GUICharacterSlot : MonoBehaviour
{
    [Serializable]
    public class Settings
    {
        public GameObject createPanel;
        public GameObject statPanel;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI classText;
        public TextMeshProUGUI raceText;
        public Button removeButton;
    }

    Settings _settings;
    CharacterData _slotData = null;

    [Inject]
    public void Constuctor(Settings settings)
    {
        _settings = settings;
    }

    public void SetupSlot(CharacterData characterData)
    {
        _slotData = characterData;
        _settings.removeButton.interactable = true;
        _settings.createPanel.SetActive(false);

        _settings.nameText.text = _slotData.name;
        _settings.classText.text = "Class: " + _slotData.classData.name;
        _settings.raceText.text = _slotData.race.name;

        _settings.statPanel.SetActive(true);
    }


    public void SetRemoveButton(CharacterServiceController controller)
    {
        _settings.removeButton.onClick.RemoveAllListeners();
        _settings.removeButton.onClick.AddListener(() =>
        {
            controller.DeleteCharacter(_slotData.name, RemoveSlot);
            _settings.removeButton.interactable = false;
        });
    }


    public void RemoveSlot()
    {
        _slotData = null;
        _settings.createPanel.SetActive(true);
        _settings.statPanel.SetActive(false);
    }
}
