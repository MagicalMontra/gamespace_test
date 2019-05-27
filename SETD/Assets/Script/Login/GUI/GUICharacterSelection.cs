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
    }
    Settings _settings;
    CharacterServiceController _controller;

    [Inject]
    public void Constructor(Settings settings, CharacterServiceController controller)
    {
        _settings = settings;
        _controller = controller;
    }

    public void Enabled(CharacterSelectSignal signal)
    {
        gameObject.SetActive(true);
        _controller.GetRaces();
    }
}
