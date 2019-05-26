using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment : Item
{
    public EquipPart part { get; private set; }
    public List<Modifier> modifiers { get; private set; }

    public Equipment(ItemData data, EquipPart part, int amount = 1, List<Modifier> modifiers = null) : base(data, amount)
    {
        this.data = data;
        this.part = part;
        this.modifiers = modifiers;
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
    Head, Body = 0, Foot, Weapon
}
