using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterSelectionInstaller : MonoInstaller<CharacterSelectionInstaller>
{
    [SerializeField] GUICharacterSelection.Settings _settings;

    public override void InstallBindings()
    {
        Container.Bind<CharacterServiceController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GUICharacterSelection.Settings>().FromInstance(_settings);

        Container.DeclareSignal<ErrorSignal>();
        Container.DeclareSignal<LogoutSignal>();
        Container.DeclareSignal<CharacterDataChangedSignal>();
        Container.DeclareSignal<CharacterNameChangedSignal>();
        Container.DeclareSignal<CreateCharacterSignal>();
        Container.DeclareSignal<CharacterCreatedSignal>();
        Container.DeclareSignal<RefreshCharacterListSignal>();

        Container.BindSignal<ErrorSignal>().ToMethod<CharacterServiceController>(c => c.OnErrorSignalFired).FromResolve();
        Container.BindSignal<CharacterSelectSignal>().ToMethod<CharacterServiceController>(c => c.OnCharacterSelectSignalFired).FromResolve();
        Container.BindSignal<CharacterDataChangedSignal>().ToMethod<CharacterServiceController>(c => c.OnCharacterDataSignalFired).FromResolve();
        Container.BindSignal<CharacterNameChangedSignal>().ToMethod<CharacterServiceController>(c => c.OnCharacterNameSignalFired).FromResolve();
        Container.BindSignal<CreateCharacterSignal>().ToMethod<CharacterServiceController>(c => c.OnCreateCharacterSignalFired).FromResolve();

        Container.BindSignal<LoadingSignal>().ToMethod<GUICharacterSelection>(gui => gui.OnLoadingSignalFired).FromResolve();
        Container.BindSignal<FinishLoading>().ToMethod<GUICharacterSelection>(gui => gui.OnFinishLoadingSignalFired).FromResolve();
        Container.BindSignal<CharacterCreatedSignal>().ToMethod<GUICharacterSelection>(gui => gui.OnCharacterCreatedSignalFired).FromResolve();
        Container.BindSignal<RefreshCharacterListSignal>().ToMethod<GUICharacterSelection>(gui => gui.OnRefreshedCharacterFired).FromResolve();
    }
}
