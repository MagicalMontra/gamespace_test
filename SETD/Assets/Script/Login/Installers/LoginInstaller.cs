using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using System;
using PlayfabServices.User;

public class LoginInstaller : MonoInstaller<LoginInstaller>
{
    [SerializeField] GUILoginController.Settings _guiSettings;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<LoginController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GUILoginController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GUICharacterSelection>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GUILoginController.Settings>().FromInstance(_guiSettings);

        Container.DeclareSignal<LoginSignal>();
        Container.DeclareSignal<RegisterSignal>();
        Container.DeclareSignal<CharacterSelectSignal>();

        Container.DeclareSignal<LoadingSignal>();
        Container.DeclareSignal<FinishLoading>();

        Container.BindSignal<LoginSignal>().ToMethod<GUILoginController>(c => c.OnLoginSignalFired).FromResolve();
        Container.BindSignal<LogoutSignal>().ToMethod<GUILoginController>(c => c.OnLogoutSignalFired).FromResolve();
        Container.BindSignal<LogoutSignal>().ToMethod<GUICharacterSelection>(c => c.OnLogoutSignalFired).FromResolve();
        Container.BindSignal<ErrorSignal>().ToMethod<GUILoginController>(c => c.OnErrorSignalFired).FromResolve();
        Container.BindSignal<RegisterSignal>().ToMethod<GUILoginController>(c => c.OnRegisterSignalFired).FromResolve();
        Container.BindSignal<CharacterSelectSignal>().ToMethod<GUICharacterSelection>(c => c.OnCharacterSelectSignalFired).FromResolve();
    }
}

