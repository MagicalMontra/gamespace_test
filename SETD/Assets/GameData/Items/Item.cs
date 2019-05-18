using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public ItemData data;
    protected int amount { get; set; }
    public Item(ItemData data, int amount)
    {
        this.data = data;
        this.amount = IncreaseAmount(amount);
    }

    protected virtual int IncreaseAmount(int inputAmount)
    {
        if (!data.isStackable)
            return 1;

        if (inputAmount + amount >= data.capacity)
            return data.capacity;

        return inputAmount + amount;
    }

    public virtual void Use()
    {
        amount--;
    }
}