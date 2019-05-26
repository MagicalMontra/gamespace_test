using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Modifier
{
    public string modName;
    public int id;
    public string modDesc;
    public float modAmount;
    public Ability ability;
    public TargetStat targetStat;
    public ModType modType;
}

public static class CalculateMod
{
    public static float IncreaseByPercentage(float amount, int statToMod)
    {
        return statToMod + ((statToMod * amount) / 100);
    }
    public static float DirectAddStat(float amount, int statToMod)
    {
        return statToMod + amount;
    }
}

[Serializable]
public enum ModType
{
    Percentage, Add, Ability
}

[Serializable]
public enum TargetStat
{
    HitPoint, Mana, Stamina, AttackPoint
}