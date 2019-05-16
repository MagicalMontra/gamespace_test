using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment : Item
{
    public Equipment(string name, string desc) : base(name, desc, 1)
    {
        itemName = name;
        itemDesc = desc;
        isStackable = false;
        IncreaseAmount(1);
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
