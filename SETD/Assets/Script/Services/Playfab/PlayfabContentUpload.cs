using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.Json;
using PlayFab.AdminModels;
using Newtonsoft.Json;

namespace PlayfabServices.Admin
{
#if UNITY_EDITOR
    public class PlayfabContentUpload
    {
        public static void UploadGameData(string key, string data)
        {
            var setTitleRequest = new SetTitleDataRequest
            {
                Key = key,
                Value = data
            };

            PlayFabAdminAPI.SetTitleData(setTitleRequest, result => Debug.Log("Upload Complete"), OnError);
        }

        public static string SetupData<T>(List<T> dataToUpload)
        {
            string json = JsonConvert.SerializeObject(dataToUpload);

            return json;
        }

        public static void OnError(PlayFabError error)
        {
#if UNITY_EDITOR
            Debug.LogError(error.GenerateErrorReport());
#else
        // Signal Error Here
#endif
        }
    }

#endif
}

