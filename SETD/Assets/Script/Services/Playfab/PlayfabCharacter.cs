using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.Json;
using PlayFab.ClientModels;
using Newtonsoft.Json;


namespace PlayfabServices.User
{
    public static class PlayfabCharacter
    {
        public static void GetCharacterDatas(Action<List<CharacterData>> dataCallback, Action<PlayFabError> errorSignal)
        {
            var listCharactersRequest = new ListUsersCharactersRequest
            {

            };

            PlayFabClientAPI.GetAllUsersCharacters(listCharactersRequest,
            result => OnGetCharacterData(result, dataCallback),
            error => OnError(error, errorSignal));
        }

        static void OnGetCharacterData(ListUsersCharactersResult result, Action<List<CharacterData>> dataCallback)
        {
            if (result.Characters.Count <= 0)
            {
                dataCallback(new List<CharacterData>());
                return;
            }

        }

        public static void CreateCharacter(string newName, string newClass, string newRace, Action<PlayFabError> errorSignal)
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "createCharacter",
                FunctionParameter = new { characterName = newName },
                GeneratePlayStreamEvent = true
            }, result => OnCharacterInitialized(result, newRace, newClass), error => OnError(error, errorSignal));
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

        static void InitializeCharacterBaseData(string characterId, string newClass, string newRace)
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
            PlayFabClientAPI.UpdateCharacterData(initCharacterRequest, OnCharacterCreated, error => Debug.Log(error.ErrorMessage));
        }

        static void OnCharacterCreated(UpdateCharacterDataResult result)
        {
            // Signal UI Change
        }

        static void OnCharacterInitialized(ExecuteCloudScriptResult result, string race, string classData)
        {
            JsonObject jsonResult = (JsonObject)result.FunctionResult;
            object message = null;
            jsonResult.TryGetValue("CharacterId", out message);
            var characterResult = JsonUtility.FromJson<GrantCharacterToUserResult>(message.ToString());

            InitializeCharacterBaseData(characterResult.CharacterId, classData, race);
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
