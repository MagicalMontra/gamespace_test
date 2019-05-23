using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabService
{
    public bool IsLoggedIn()
    {
        return PlayFabClientAPI.IsClientLoggedIn();
    }

    internal virtual void OnError(PlayFabError error)
    {
#if UNITY_EDITOR
        Debug.LogError(error.GenerateErrorReport());
#else
        // Signal Error Here
#endif
    }
}
