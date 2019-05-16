using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public string itemDesc;
    public bool isStackable;
    private int amount { get; set; }
    private int maxAmount { get; set; }

    public Item(string name, string desc, int amount, bool canBeStacked = true)
    {
        itemName = name;
        itemDesc = desc;
        isStackable = canBeStacked;
        this.amount = IncreaseAmount(amount);
    }

    protected virtual int IncreaseAmount(int inputAmount)
    {
        if (!isStackable)
            return 1;

        if (inputAmount + amount >= maxAmount)
            return maxAmount;

        return inputAmount + amount;
    }

    public virtual void Use()
    {
        amount--;
    }
}