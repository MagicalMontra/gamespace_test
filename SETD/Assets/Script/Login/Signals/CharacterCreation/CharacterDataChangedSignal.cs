using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataChangedSignal
{
    public string id;
    public TargetData targetData { get; private set; }

    public CharacterDataChangedSignal(string id, TargetData targetData)
    {
        this.id = id;
        this.targetData = targetData;
    }
}

public enum TargetData
{
    Race, Class
};
