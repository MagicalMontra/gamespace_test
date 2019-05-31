using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayfabServices.User;
using PlayFab.ClientModels;
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

    public void DeleteCharacter(string name, Action onDelete)
    {
        PlayfabCharacter.DeleteCharacter(name, onDelete, OnError);
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
        _signalBus.TryFire(new LoadingSignal("Pulling character list from server"));
    }

    void OnGetRace(List<Race> races)
    {
        _races = races;
        _newCharacter.race = _races.FirstOrDefault();
        _dataFlag.succeedGetRace = true;
    }

    void OnGetClass(List<ClassData> classes)
    {
        _classes = classes;
        _newCharacter.classData = _classes.FirstOrDefault();
        _dataFlag.succeedGetData = true;
    }

    void OnGetCharacter(ListUsersCharactersResult result)
    {
        if (result.Characters.Count <= 0)
        {
            _dataFlag.succeedGetCharacter = true;
            return;
        }

        StartCoroutine(GetEachCharacterData(result));
    }

    IEnumerator GetEachCharacterData(ListUsersCharactersResult result)
    {
        _characters.Clear();

        for (int i = 0; i < result.Characters.Count; i++)
        {
            PlayfabCharacter.GetCharacterDeepData(result.Characters[i].CharacterId,
            result.Characters[i].CharacterName,
            cResult => _characters.Add(cResult), OnError);
        }

        yield return new WaitUntil(() => _characters.Count >= result.Characters.Count);

        _dataFlag.succeedGetCharacter = true;
        _signalBus.TryFire(new FinishLoading());
        _signalBus.TryFire(new RefreshCharacterListSignal(_characters));
    }

    void OnError(PlayFabError error)
    {
        _signalBus.TryFire(new ErrorSignal(error.ErrorMessage));
    }

    void OnCreateCharacterSucceed()
    {
        CallCharacters();
        _signalBus.TryFire(new CharacterCreatedSignal());
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
        if (signal.targetData == TargetData.Race)
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

    public void OnCreateCharacterSignalFired(CreateCharacterSignal signal)
    {
        string jsonClass = PlayfabCharacter.SetupData(_newCharacter.classData);
        string jsonRace = PlayfabCharacter.SetupData(_newCharacter.race);
        PlayfabCharacter.CreateCharacter(_newCharacter.name, jsonRace, jsonClass, OnCreateCharacterSucceed, OnError);
    }
}
