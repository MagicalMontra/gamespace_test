using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorSignal
{
    public string _errorMessage { get; private set; }

    public ErrorSignal(string errorMessage)
    {
        _errorMessage = errorMessage;
    }
}
