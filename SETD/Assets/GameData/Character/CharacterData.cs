using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public List<Modifier> modifiers = new List<Modifier>();
    public Race race;
    public ClassData classData;
    public string name;
    int _maxHitPoints;
    int _maxMana;
    int _maxStamina;
    float _hitPoints;
    float _manaPoints;
    float _staminaPoints;


    public void InitResources()
    {
        _hitPoints = _maxHitPoints;

        // Some character may not have mana
        if (_maxMana > 0)
            _manaPoints = _maxMana;

        _staminaPoints = _maxStamina;
    }

    public float HitPointsChange(float changeAmount)
    {
        var calculatedValue = ResourceChange(_hitPoints, changeAmount, _maxHitPoints);
        return _hitPoints = calculatedValue;
    }

    public float ManaPointsChange(float changeAmount)
    {
        var calculatedValue = ResourceChange(_manaPoints, changeAmount, _maxMana);
        return _manaPoints = calculatedValue;
    }

    public float StaminaChange(float changeAmount)
    {
        var calculatedValue = ResourceChange(_manaPoints, changeAmount, _maxStamina);
        return _staminaPoints = calculatedValue;
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
