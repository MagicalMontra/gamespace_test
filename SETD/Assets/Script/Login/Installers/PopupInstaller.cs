using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using System;

public class PopupInstaller : MonoInstaller<PopupInstaller>
{
    [SerializeField]
    GUILoginPopup.Settings _settings;

    public override void InstallBindings()
    {
        Container.Bind<GUILoginPopup.Settings>().FromInstance(_settings);
    }
}

