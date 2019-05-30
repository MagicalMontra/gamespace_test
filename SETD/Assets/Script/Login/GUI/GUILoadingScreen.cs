using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using System;

public class GUILoadingScreen : MonoBehaviour
{
    [Serializable]
    public class Settings
    {
        public TextLoadingAnimation loadingAnim;
    }

    Settings _settings;

    [Inject]
    public void Constructor(Settings settings)
    {
        _settings = settings;
    }

    public void StartLoading()
    {
        gameObject.SetActive(true);
        _settings.loadingAnim.StartTextSequence();
    }

    public void EndLoading()
    {
        gameObject.SetActive(false);
        _settings.loadingAnim.StopTextSequence();
    }

}
