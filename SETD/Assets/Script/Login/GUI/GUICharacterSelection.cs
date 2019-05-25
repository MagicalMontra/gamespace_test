using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUICharacterSelection : MonoBehaviour
{
    [Serializable]
    public class Settings
    {
        public Button startButton;
        public Button logoutButton;
    }
    Settings _settings;

    public void Enabled(CharacterSelectSignal signal)
    {
        gameObject.SetActive(true);
    }
}
