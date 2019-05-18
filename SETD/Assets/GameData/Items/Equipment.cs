using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment : Item
{
    public Equipment(ItemData data, int amount = 1) : base(data, amount)
    {
        this.data = data;
        this.amount = IncreaseAmount(amount);
    }

    public override void Use()
    {
        return;
    }

    public Equipment Equip()
    {
        return this;
    }
}

[System.Serializable]
public enum EquipPart
{
    Head, Body, Foot, Weapon
}
