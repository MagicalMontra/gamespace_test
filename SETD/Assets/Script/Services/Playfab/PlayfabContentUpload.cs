﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.Json;
using PlayFab.AdminModels;

namespace PlayfabServices.Admin
{
#if UNITY_EDITOR
    public class PlayfabContentUpload
    {
        public static void UploadGameData(string key, JsonObject data)
        {
            var setTitleRequest = new SetTitleDataRequest
            {
                Key = key,
                Value = data.ToString()
            };

            PlayFabAdminAPI.SetTitleData(setTitleRequest, result => Debug.Log("Upload Complete"), OnError);
        }

        public static JsonObject SetupData(List<Modifier> dataToUpload)
        {
            JsonObject newdata = new JsonObject();
            foreach (var data in dataToUpload)
            {
                JsonObject attribute = new JsonObject();
                foreach (var method in data.GetType().GetFields())
                {
                    if (method.Name != "modName" && !string.IsNullOrEmpty(method.GetValue(data).ToString()))
                        attribute.Add(method.Name, method.GetValue(data).ToString());
                }

                newdata.Add(data.modName, attribute);
            }

            return newdata;
        }

        public static JsonObject SetupData(List<ClassData> dataToUpload)
        {
            JsonObject newdata = new JsonObject();
            foreach (var data in dataToUpload)
            {
                JsonObject attribute = new JsonObject();
                foreach (var method in data.GetType().GetFields())
                {
                    if (method.Name == "id")
                        attribute.Add(method.Name, method.GetValue(data).ToString());
                }

                JsonObject modifiers = new JsonObject();
                foreach (var classMod in data.modifiers)
                {
                    modifiers.Add("modName", classMod.modName);
                    modifiers.Add("id", classMod.id.ToString());
                }
                attribute.Add("Modifiers", modifiers);
                newdata.Add(data.name, attribute);
            }

            return newdata;
        }

        public static JsonObject SetupData(List<Race> dataToUpload)
        {
            JsonObject newdata = new JsonObject();
            foreach (var data in dataToUpload)
            {
                JsonObject attribute = new JsonObject();
                foreach (var method in data.GetType().GetFields())
                {
                    if (method.Name == "id")
                        attribute.Add(method.Name, method.GetValue(data).ToString());
                }

                JsonObject modifiers = new JsonObject();
                foreach (var classMod in data.modifiers)
                {
                    modifiers.Add("modName", classMod.modName);
                    modifiers.Add("id", classMod.id.ToString());
                }
                attribute.Add("Modifiers", modifiers);
                newdata.Add(data.name, attribute);
            }

            return newdata;
        }

        // public Dictionary<string, string> SetupData(List<Race> dataToUpload)
        // {
        //     Dictionary<string, string> keypairs = new Dictionary<string, string>();
        // }

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

