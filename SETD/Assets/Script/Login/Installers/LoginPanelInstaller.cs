using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoginPanelInstaller : MonoInstaller<LoginPanelInstaller>
{

    [SerializeField] GUILoginPanel.Settings _settings;

    public override void InstallBindings()
    {
        Container.Bind<GUILoginPanel.Settings>().FromInstance(_settings);
    }
}
