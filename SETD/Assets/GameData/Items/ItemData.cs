﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int id;
    public string itemName = "";
    public string itemDesc = "";
    public bool isStackable;
    public int capacity;
    public int price;
}
