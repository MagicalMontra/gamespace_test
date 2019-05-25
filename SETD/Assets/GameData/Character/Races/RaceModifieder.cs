using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaceModifier : Modifier
{
    public RaceModifier(IModType type, string name, string desc, int modAmount) : base(type, name, desc, modAmount)
    {

    }
}
