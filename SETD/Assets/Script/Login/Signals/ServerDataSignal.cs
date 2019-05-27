using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerDataSignal<T>
{
    public bool _isValid { get; private set; }
    public List<T> _listData { get; private set; }

    public ServerDataSignal(bool isValid, List<T> listData)
    {
        _isValid = isValid;
        _listData = listData;
    }
}
