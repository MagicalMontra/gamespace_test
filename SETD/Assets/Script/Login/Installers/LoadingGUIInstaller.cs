using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoadingGUIInstaller : MonoInstaller<LoadingGUIInstaller>
{
    [SerializeField]
    GUILoadingScreen.Settings settings;

    public override void InstallBindings()
    {
        Container.Bind<GUILoadingScreen.Settings>().FromInstance(settings).AsTransient();
    }
}
