using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterCreatePanelInstaller : MonoInstaller<CharacterCreatePanelInstaller>
{

    [SerializeField] GUICharacterCreate.Settings _setting;

    public override void InstallBindings()
    {
        Container.Bind<GUICharacterCreate.Settings>().FromInstance(_setting);
    }
}
