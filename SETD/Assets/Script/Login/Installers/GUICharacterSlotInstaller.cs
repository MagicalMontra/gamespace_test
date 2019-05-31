using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GUICharacterSlotInstaller : MonoInstaller<GUICharacterSlotInstaller>
{
    [SerializeField]
    GUICharacterSlot.Settings _settings;
    public override void InstallBindings()
    {
        Container.Bind<GUICharacterSlot.Settings>().FromInstance(_settings);
    }
}
