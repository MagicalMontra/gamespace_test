using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNameChangedSignal
{
    public string name { get; private set; }

    public CharacterNameChangedSignal(string name)
    {
        this.name = name;
    }
}
