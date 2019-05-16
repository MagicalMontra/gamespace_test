using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    public ModType modType;
    public string modName;
    public string modDesc;
    public int amount;
}

[System.Serializable]
public enum ModType
{
    Add = 0, Percentage, Benefit
}
