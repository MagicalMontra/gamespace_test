using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Race
{
    public string name;
    public int id;
    public List<Modifier> modifiers = new List<Modifier>();
    public List<Ability> abilities = new List<Ability>();
}
