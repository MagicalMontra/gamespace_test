using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.Json;
using PlayFab.ClientModels;


namespace PlayfabServices.User
{
    public class PlayfabCharacter : PlayfabService
    {
        void CreateCharacter(string newName, string newClass, string newRace)
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "createCharacter",
                FunctionParameter = new { characterName = newName },
                GeneratePlayStreamEvent = true
            }, OnCharacterInitialized, OnError);
        }

        void OnCharacterCreated(UpdateCharacterDataResult result)
        {
            // Signal UI Change
        }

        void OnCharacterInitialized(ExecuteCloudScriptResult result)
        {
            JsonObject jsonResult = (JsonObject)result.FunctionResult;
            object message = null;
            jsonResult.TryGetValue("CharacterId", out message);
            var characterResult = JsonUtility.FromJson<GrantCharacterToUserResult>(message.ToString());

            InitializeCharacterBaseData(characterResult.CharacterId, "", "");
        }

        void InitializeCharacterBaseData(string characterId, string newClass, string newRace)
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
            PlayFabClientAPI.UpdateCharacterData(initCharacterRequest, OnCharacterCreated, OnError);
        }
    }

}
