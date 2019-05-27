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
        public static void CreateCharacter(string newName, string newClass, string newRace, Action<PlayFabError> errorSignal)
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "createCharacter",
                FunctionParameter = new { characterName = newName },
                GeneratePlayStreamEvent = true
            }, OnCharacterInitialized, error => OnError(error, errorSignal));
        }

        public static void GetServerTitleData<T>(string key, Action<List<T>> signal, Action<PlayFabError> errorSignal)
        {
            var request = new GetTitleDataRequest()
            {
                Keys = new List<string>() { key }
            };

            PlayFabClientAPI.GetTitleData(request, result => OnGetTitleData<T>(key, result, signal), error => OnError(error, errorSignal));
        }

        static void OnGetTitleData<T>(string key, GetTitleDataResult result, Action<List<T>> signal)
        {
            string json = "";
            result.Data.TryGetValue(key, out json);
            SendData<T>(json, signal);
        }

        static void SendData<T>(string json, Action<List<T>> signal)
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json);
            signal(list);
        }

        static void InitializeCharacterBaseData(string characterId, string newClass, string newRace, Action<PlayFabError> errorSignal)
        {
            var newCharacterData = new Dictionary<string, string>();
            newCharacterData.Add("Class", newClass);
            newCharacterData.Add("Race", newRace);

            var initCharacterRequest = new UpdateCharacterDataRequest()
            {
                CharacterId = characterId,
                Data = newCharacterData,
                Permission = UserDataPermission.Public
            };
            PlayFabClientAPI.UpdateCharacterData(initCharacterRequest, OnCharacterCreated, error => OnError(error, errorSignal));
        }

        static void OnCharacterCreated(UpdateCharacterDataResult result)
        {
            // Signal UI Change
        }

        static void OnCharacterInitialized(ExecuteCloudScriptResult result)
        {
            JsonObject jsonResult = (JsonObject)result.FunctionResult;
            object message = null;
            jsonResult.TryGetValue("CharacterId", out message);
            var characterResult = JsonUtility.FromJson<GrantCharacterToUserResult>(message.ToString());

            // InitializeCharacterBaseData(characterResult.CharacterId, "", "");
        }

        static void OnError(PlayFabError error, Action<PlayFabError> signal)
        {

        }
    }

}
