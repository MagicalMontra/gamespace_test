using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        PlayfabCharacter.GetLiveRace();
    }
}
