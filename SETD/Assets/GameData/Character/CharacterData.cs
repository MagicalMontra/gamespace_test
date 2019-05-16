using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public Race race;
    public ClassData classData;
    public List<Modifier> modifiers;
    public string name;
    public int maxHitPoints = 10;
    public int maxMana = 10;
    public int maxStamina = 100;

    float _hp;
    float _mp = 0;
    float _sp;

    public float HitPoints
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = ResourceChange(_hp, value, maxHitPoints);
        }
    }

    public float ManaPoints
    {
        get
        {
            return _mp;
        }
        set
        {
            _mp = ResourceChange(_mp, value, maxMana);
        }
    }

    public float StaminaPoints
    {
        get
        {
            return _sp;
        }
        set
        {
            _sp = ResourceChange(_sp, value, maxStamina);
        }
    }

    public void InitResources()
    {
        _hp = maxHitPoints;

        // Some character may not have mana
        if (maxMana > 0)
            _mp = maxMana;

        _sp = maxStamina;
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
