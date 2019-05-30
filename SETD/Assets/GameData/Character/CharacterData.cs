using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public List<Modifier> modifiers = new List<Modifier>();
    public Race race { get; private set; }
    public ClassData classData { get; private set; }
    public string name { get; private set; }
    public int maxHitPoints { get; private set; }
    public int maxMana { get; private set; }
    public int maxStamina { get; private set; }

    public float hitPoints { get; private set; }
    public float manaPoints { get; private set; }
    public float staminaPoints { get; private set; }

    public void InitResources()
    {
        hitPoints = maxHitPoints;

        // Some character may not have mana
        if (maxMana > 0)
            manaPoints = maxMana;

        staminaPoints = maxStamina;
    }

    public float HitPointsChange(float changeAmount)
    {
        var calculatedValue = ResourceChange(hitPoints, changeAmount, maxHitPoints);
        return hitPoints = calculatedValue;
    }

    public float ManaPointsChange(float changeAmount)
    {
        var calculatedValue = ResourceChange(manaPoints, changeAmount, maxMana);
        return manaPoints = calculatedValue;
    }

    public float StaminaChange(float changeAmount)
    {
        var calculatedValue = ResourceChange(manaPoints, changeAmount, maxStamina);
        return staminaPoints = calculatedValue;
    }

    protected float ResourceChange(float targetResource, float changedAmount, int resourceLimits)
    {
        // return calculated amount

        if (targetResource + changedAmount >= resourceLimits)
            return resourceLimits;

        if (targetResource + changedAmount <= 0)
            return 0;

        return targetResource + changedAmount;
    }
}
