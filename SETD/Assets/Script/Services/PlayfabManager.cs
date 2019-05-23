using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayfabManager : MonoBehaviour
{
    [SerializeField]
    PlayfabService _service;

    [Inject]
    public void Constructor(PlayfabService service)
    {
        _service = service as PlayfabService;
    }

    public PlayfabService GetService()
    {
        return _service;
    }
}
