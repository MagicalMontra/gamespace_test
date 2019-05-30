using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayfabServices.User;
using Zenject;

public class CharacterServiceController : MonoBehaviour
{

    struct DataFlag
    {
        public bool succeedGetRace;
        public bool succeedGetData;
        public bool succeedGetCharacter;

        public bool IsAllTrue()
        {
            return succeedGetRace && succeedGetData && succeedGetCharacter;
        }
    }

    struct NewCharacter
    {
        public string name;
        public Race race;
        public ClassData classData;
    }

    NewCharacter _newCharacter;
    DataFlag _dataFlag;
    SignalBus _signalBus;
    List<Race> _races = new List<Race>();
    List<ClassData> _classes = new List<ClassData>();
    List<CharacterData> _characters = new List<CharacterData>();

    [Inject]
    public void Constructor(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public List<Race> GetRaces()
    {
        return _races;
    }

    public List<ClassData> GetClasses()
    {
        return _classes;
    }

    IEnumerator OnGotAllData()
    {
        yield return new WaitUntil(() => _dataFlag.IsAllTrue());
        _signalBus.TryFire(new FinishLoading());
    }

    void CallRaces()
    {
        PlayfabCharacter.GetServerTitleData<Race>("Race", OnGetRace, OnError);
    }

    void CallClasses()
    {
        PlayfabCharacter.GetServerTitleData<ClassData>("ClassData", OnGetClass, OnError);
    }

    void CallCharacters()
    {
        PlayfabCharacter.GetCharacterDatas(OnGetCharacter, OnError);
    }

    void OnGetRace(List<Race> races)
    {
        _races = races;
        _dataFlag.succeedGetRace = true;
    }

    void OnGetClass(List<ClassData> classes)
    {
        _classes = classes;
        _dataFlag.succeedGetData = true;
    }

    void OnGetCharacter(List<CharacterData> characters)
    {
        _dataFlag.succeedGetCharacter = true;

        if (characters.Count <= 0)
            return;

        _characters = characters;
    }

    void OnError(PlayFabError error)
    {
        _signalBus.TryFire(new ErrorSignal(error.ErrorMessage));
    }

    public void OnCharacterSelectSignalFired(CharacterSelectSignal signal)
    {
        _signalBus.TryFire(new LoadingSignal("Getting data from server"));
        CallCharacters();
        CallRaces();
        CallClasses();
        StartCoroutine(OnGotAllData());
    }

    public void OnErrorSignalFired(ErrorSignal signal)
    {
        _signalBus.TryFire(new LogoutSignal());
    }

    public void OnCharacterNameSignalFired(CharacterNameChangedSignal signal)
    {
        _newCharacter.name = signal.name;
    }

    public void OnCharacterDataSignalFired(CharacterDataChangedSignal signal)
    {
        if (signal.targetData == TargetData.Class)
        {
            Race raceToCreate = _races.Find(race => race.id == Convert.ToInt32(signal.id));
            _newCharacter.race = raceToCreate;
            Debug.Log(raceToCreate.name);
        }
        else
        {
            ClassData classToCreate = _classes.Find(c => c.id == Convert.ToInt32(signal.id));
            _newCharacter.classData = classToCreate;
            Debug.Log(classToCreate.name);
        }
    }

    public void OnCharacterCreateSignalFired(CharacterCreateSignal signal)
    {
        string jsonClass = PlayfabCharacter.SetupData(_newCharacter.classData);
        string jsonRace = PlayfabCharacter.SetupData(_newCharacter.race);
        PlayfabCharacter.CreateCharacter(_newCharacter.name, jsonRace, jsonClass, OnError);
    }
}
