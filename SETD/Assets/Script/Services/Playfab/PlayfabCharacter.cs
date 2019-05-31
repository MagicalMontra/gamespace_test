using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.Json;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using DG.Tweening;


namespace PlayfabServices.User
{
    public static class PlayfabCharacter
    {
        public static void GetCharacterDatas(Action<ListUsersCharactersResult> dataCallback, Action<PlayFabError> errorSignal)
        {
            var listCharactersRequest = new ListUsersCharactersRequest
            {

            };

            PlayFabClientAPI.GetAllUsersCharacters(listCharactersRequest,
            result => OnGetCharacterData(result, dataCallback),
            error => OnError(error, errorSignal));
        }

        public static void GetCharacterDeepData(string id, string name, Action<CharacterData> dataCallback, Action<PlayFabError> errorSignal)
        {
            var characterDataRequest = new GetCharacterDataRequest
            {
                CharacterId = id,
                Keys = new List<string> { "Race", "ClassData" }
            };

            PlayFabClientAPI.GetCharacterData(characterDataRequest, result =>
            {
                CharacterData data = new CharacterData();
                UserDataRecord race = new UserDataRecord();
                UserDataRecord classData = new UserDataRecord();
                result.Data.TryGetValue("Race", out race);
                result.Data.TryGetValue("ClassData", out classData);

                data.name = name;
                data.race = JsonConvert.DeserializeObject<Race>(race.Value);
                data.classData = JsonConvert.DeserializeObject<ClassData>(classData.Value);

                dataCallback(data);
            }, error => OnError(error, errorSignal));
        }

        static void OnGetCharacterData(ListUsersCharactersResult result,
        Action<ListUsersCharactersResult> dataCallback)
        {
            dataCallback(result);
        }

        public static void CreateCharacter(string newName, string newClass, string newRace, Action characterCreated, Action<PlayFabError> errorSignal)
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "createCharacter",
                FunctionParameter = new { characterName = newName },
                GeneratePlayStreamEvent = true
            }, result => OnCharacterInitialized(result, newRace, newClass, characterCreated), error => OnError(error, errorSignal));
        }

        public static void DeleteCharacter(string charName, Action onDelete, Action<PlayFabError> errorSignal)
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "removeCharacter",
                FunctionParameter = new { charToDelete = charName },
                GeneratePlayStreamEvent = true
            }, result => onDelete(), error => OnError(error, errorSignal));
        }

        public static string SetupData<T>(T dataToUpload)
        {
            string json = JsonConvert.SerializeObject(dataToUpload);

            return json;
        }

        public static void GetServerTitleData<T>(string key, Action<List<T>> dataCallback, Action<PlayFabError> errorSignal)
        {
            var request = new GetTitleDataRequest()
            {
                Keys = new List<string>() { key }
            };

            PlayFabClientAPI.GetTitleData(request, result => OnGetTitleData<T>(key, result, dataCallback, errorSignal), error => OnError(error, errorSignal));
        }

        static void OnGetTitleData<T>(string key, GetTitleDataResult result, Action<List<T>> dataCallback, Action<PlayFabError> errorSignal)
        {
            if (result.Data.Count <= 0)
            {
                var error = new PlayFabError();
                error.ErrorMessage = "No Data Found";
                OnError(error, errorSignal);
                return;
            }

            string json = "";
            result.Data.TryGetValue(key, out json);
            SendData<T>(json, dataCallback);
        }

        static void SendData<T>(string json, Action<List<T>> dataCallback)
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json);
            dataCallback(list);
        }

        static void InitializeCharacterBaseData(string characterId, string newClass, string newRace, Action characterCreated)
        {
            var newCharacterData = new Dictionary<string, string>();
            newCharacterData.Add("Race", newRace);
            newCharacterData.Add("ClassData", newClass);

            var initCharacterRequest = new UpdateCharacterDataRequest()
            {
                CharacterId = characterId,
                Data = newCharacterData,
                Permission = UserDataPermission.Public
            };

            PlayFabClientAPI.UpdateCharacterData(initCharacterRequest, result => OnCharacterCreated(result, characterCreated),
            error => Debug.Log(error.ErrorMessage));
        }

        static void OnCharacterCreated(UpdateCharacterDataResult result, Action signal)
        {
            // Signal UI Change
            signal();
        }

        static void OnCharacterInitialized(ExecuteCloudScriptResult result, string race, string classData, Action characterCreated)
        {
            JsonObject jsonResult = (JsonObject)result.FunctionResult;
            object message = null;
            jsonResult.TryGetValue("CharacterId", out message);
            var characterResult = JsonUtility.FromJson<GrantCharacterToUserResult>(message.ToString());

            InitializeCharacterBaseData(characterResult.CharacterId, classData, race, characterCreated);
        }

        static void OnError(PlayFabError error, Action<PlayFabError> signal)
        {
            signal(error);

#if UNITY_EDITOR
            Debug.Log(error.ErrorMessage);
#endif
        }
    }

}
