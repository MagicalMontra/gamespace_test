using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassModifier : Modifier
{
    public ClassModifier(IModType type, string name, string desc, int modAmount) : base(type, name, desc, modAmount)
    {

    }
}
