using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSignal
{
    public bool _isLoggedin { get; private set; }
    string _detail;

    public LoginSignal(bool isLoggedIn, string detail)
    {
        _isLoggedin = isLoggedIn;
        _detail = detail;
    }

    public string GetContent()
    {
        if (!_isLoggedin)
        {
            return "Error with" + _detail;
        }

        return _detail;
    }
}
