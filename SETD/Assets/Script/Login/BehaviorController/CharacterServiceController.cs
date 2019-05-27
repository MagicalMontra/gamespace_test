using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayfabServices.User;
using Zenject;

public class CharacterServiceController : MonoBehaviour
{
    SignalBus _signalBus;
    CharacterData _newCharacter;

    [Inject]
    public void Constructor(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void GetRaces()
    {
        PlayfabCharacter.GetServerTitleData<Race>("Race", OnGetRace, OnError);
    }

    public void GetClass()
    {
        PlayfabCharacter.GetServerTitleData<Race>("ClassData", OnGetRace, OnError);
    }

    public void OnGetRace(List<Race> races)
    {
        _signalBus.TryFire(new ServerDataSignal<Race>(true, races));
    }

    void OnGetClass(List<ClassData> classes)
    {
        _signalBus.TryFire(new ServerDataSignal<ClassData>(true, classes));
    }

    void OnError(PlayFabError error)
    {
        _signalBus.TryFire(new ServerDataSignal<object>(false, null));
    }

    public void OnGetRaceSignalFired(ServerDataSignal<Race> signal)
    {
        Debug.Log(signal._listData.Find(c => c.name == "Elf").name);
    }
}
