using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterSelectionInstaller : MonoInstaller<CharacterSelectionInstaller>
{
    [SerializeField] GUICharacterSelection.Settings _settings;

    public override void InstallBindings()
    {
        Container.Bind<GUICharacterSelection.Settings>().FromInstance(_settings);
    }
}
