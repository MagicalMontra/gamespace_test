using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterSignal
{
    public bool _isValid { get; private set; }
    string _detail;

    public RegisterSignal(bool isValid, string detail)
    {
        _isValid = isValid;
        _detail = detail;
    }

    public string GetContent()
    {
        if (!_isValid)
        {
            return "Error with" + _detail;
        }

        return _detail;
    }
}
