using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Modifier
{
    public IModType modType { get; private set; }
    public string modName { get; private set; }
    public string modDesc { get; private set; }
    public float modAmount { get; private set; }

    public Modifier(IModType type, string name, string desc, int modAmount)
    {

    }
}

public interface IModType
{

}

public interface IStatMod : IModType
{
    float CalculateModifyStat(float amount, int statToMod);
}

[Serializable]
public class PercentageMod : IStatMod
{
    public TargetStat targetStat { get; private set; }

    public PercentageMod(TargetStat targetStat)
    {
        this.targetStat = targetStat;
    }
    public float CalculateModifyStat(float amount, int statToMod)
    {
        return statToMod + ((statToMod * amount) / 100);
    }
}

[Serializable]
public class AddMod : IStatMod
{
    public TargetStat targetStat { get; private set; }

    public AddMod(TargetStat targetStat)
    {
        this.targetStat = targetStat;
    }
    public float CalculateModifyStat(float amount, int statToMod)
    {
        return statToMod + amount;
    }
}

[Serializable]
public class AbilityMod : IModType
{
    public Ability ability { get; private set; }

    public AbilityMod(Ability ability)
    {
        this.ability = ability;
    }
}

[Serializable]
public enum TargetStat
{
    HitPoint, Mana, Stamina, AttackPoint, Ability
}