using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Zenject;

namespace PlayfabServices.User
{
    public static class PlayfabLogin
    {
        #region API

        public static void Login(string username, string password, Action<LoginResult> signal, Action<PlayFabError> errorSignal)
        {
            var loginRequest = new LoginWithPlayFabRequest
            {
                Username = username,
                Password = password
            };

            PlayFabClientAPI.LoginWithPlayFab(loginRequest, result => OnLoginComplete(result, signal), error => OnError(error, errorSignal));
        }
        public static void Register(string username, string password, Action<RegisterPlayFabUserResult> signal, Action<PlayFabError> errorSignal)
        {
            var registerRequest = new RegisterPlayFabUserRequest
            {
                TitleId = PlayFabSettings.TitleId,
                Username = username,
                Password = password,
                RequireBothUsernameAndEmail = false,
            };

            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, result => OnRegisterComplete(result, signal), error => OnError(error, errorSignal));
        }

        #endregion

        #region OnResponse

        static void OnRegisterComplete(RegisterPlayFabUserResult result, Action<RegisterPlayFabUserResult> signal)
        {
            // Signal UI Changes here
#if UNITY_EDITOR
            Debug.Log(result.PlayFabId + " account has been created.");
#endif
            signal(result);
        }

        static void OnLoginComplete(LoginResult result, Action<LoginResult> signal)
        {
            // Signal UI Changes here
#if UNITY_EDITOR
            Debug.Log(result.PlayFabId + " logged in.");
#endif

            signal(result);
        }

        static void OnError(PlayFabError error, Action<PlayFabError> signal)
        {
            signal(error);
        }

        #endregion
    }

}