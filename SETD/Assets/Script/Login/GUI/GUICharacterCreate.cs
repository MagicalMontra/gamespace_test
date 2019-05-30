using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using System;

public class GUICharacterCreate : MonoBehaviour
{
    Settings _setting;
    SignalBus _signalBus;
    List<GameObject> modTabs = new List<GameObject>();

    CharacterServiceController _controller;

    [Inject]
    public void Constructor(Settings settings, SignalBus signalBus, CharacterServiceController controller)
    {
        _setting = settings;
        _signalBus = signalBus;
        _controller = controller;
        AssignInputFunction();
    }

    void AssignInputFunction()
    {
        _setting.cancelButton.onClick.AddListener(() => gameObject.SetActive(false));
        _setting.sumButton.onClick.AddListener(() => OnSummaryOpened());
        _setting.createButton.onClick.AddListener(() => OnCreateButtonClicked());

        _setting.nameInput.onValueChanged.AddListener(value => OnCharacterNameChanged());
        _setting.raceInput.onValueChanged.AddListener(value => OnRaceChanged());
        _setting.classInput.onValueChanged.AddListener(value => OnClassChanged());
    }

    public void SetupRace()
    {
        var races = _controller.GetRaces();
        List<string> options = new List<string>();

        for (int i = 0; i < races.Count; i++)
        {
            options.Add(races[i].name);
        }

        _setting.raceInput.ClearOptions();
        _setting.raceInput.AddOptions(options);
    }

    public void SetupClass()
    {
        var classes = _controller.GetClasses();
        List<string> options = new List<string>();

        for (int i = 0; i < classes.Count; i++)
        {
            options.Add(classes[i].name);
        }

        _setting.classInput.ClearOptions();
        _setting.classInput.AddOptions(options);
    }

    void OnCharacterNameChanged()
    {
        _setting.sumName.text = _setting.nameInput.text;
        _signalBus.TryFire(new CharacterNameChangedSignal(_setting.nameInput.text));
    }

    void OnRaceChanged()
    {
        _setting.sumRace.text = _setting.raceInput.itemText.text;
        _signalBus.TryFire(new CharacterDataChangedSignal(_setting.raceInput.value.ToString(), TargetData.Race));
    }

    void OnClassChanged()
    {
        _setting.sumClass.text = _setting.classInput.itemText.text;
        _signalBus.TryFire(new CharacterDataChangedSignal(_setting.classInput.value.ToString(), TargetData.Class));
    }

    void OnSummaryOpened()
    {
        _setting.sumPanel.SetActive(true);
        _setting.createPanel.SetActive(false);
    }

    void OnCreateButtonClicked()
    {
        _signalBus.TryFire(new CharacterCreateSignal());
    }

    [Serializable]
    public class Settings
    {
        public TMP_InputField nameInput;
        public Dropdown raceInput;
        public Dropdown classInput;
        public TextMeshProUGUI sumName;
        public TextMeshProUGUI sumRace;
        public TextMeshProUGUI sumClass;
        public TextMeshProUGUI sumHP;
        public TextMeshProUGUI sumMP;
        public TextMeshProUGUI sumSP;
        public Button cancelButton;
        public Button sumButton;
        public Button createButton;
        public GameObject createPanel;
        public GameObject sumPanel;
        public GameObject modPrefab;
    }
}
