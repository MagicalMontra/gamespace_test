using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataScriptable", menuName = "Item Data/Create Scriptable")]
[Serializable]
public class ItemDataScriptableMeta : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();
    public Version tableVersion;
    public string targetTable;

    public void AddItem(ItemData itemToCreate)
    {
        items.Add(itemToCreate);
    }

    public void DeleteItem(ItemData itemToDelete)
    {
        items.Remove(itemToDelete);
    }
}
