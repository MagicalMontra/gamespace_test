using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.Json;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    #region  API

    #region  Connection Resolver

    // IEnumerator ConnectionResolver(Action api)
    // {
    //     api();
    // }

    #endregion

    // async Task LoginTask(object task)
    // {
    //     await task();
    // }
    public void OnLoginButtonClicked()
    {
        PlayfabLogin("test001", "111111");
    }

    public void OnRegisterButtonCLicked()
    {
        PlayfabRegister("test001", "111111");
    }

    void PlayfabRegister(string username, string password)
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            TitleId = "18CA2",
            Username = username,
            Password = password,
            RequireBothUsernameAndEmail = false,
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterCallback, ErrorCallback);
    }

    void PlayfabLogin(string username, string password)
    {
        var loginRequest = new LoginWithPlayFabRequest
        {
            Username = username,
            Password = password
        };

        PlayFabClientAPI.LoginWithPlayFab(loginRequest, LoginCallback, ErrorCallback);
    }

    #endregion

    #region  Callback

    void RegisterCallback(RegisterPlayFabUserResult result)
    {
        Debug.Log("Created" + result.PlayFabId + " account completed");
    }

    void LoginCallback(LoginResult loginResult)
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "createCharacter",
            FunctionParameter = new { characterName = "TestSubject" },
            GeneratePlayStreamEvent = true
        }, csResult =>
        {
            JsonObject jsonResult = (JsonObject)csResult.FunctionResult;
            object message = null;
            jsonResult.TryGetValue("CharacterId", out message);
            var characterResult = JsonUtility.FromJson<GrantCharacterToUserResult>(message.ToString());

            var newCharacterData = new Dictionary<string, string>();
            newCharacterData.Add("Class", "Swordman");
            newCharacterData.Add("Race", "Human");
            var initCharacterRequest = new UpdateCharacterDataRequest()
            {
                CharacterId = characterResult.CharacterId,
                Data = newCharacterData,
                Permission = UserDataPermission.Public
            };
            PlayFabClientAPI.UpdateCharacterData(initCharacterRequest, null, ErrorCallback);
        }, ErrorCallback);
    }

    void ErrorCallback(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport().ToString());
    }

    #endregion
}
