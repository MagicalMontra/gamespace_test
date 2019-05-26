using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Race
{
    public string name { get; private set; }
    public List<Modifier> modifiers { get; private set; }
    public List<Ability> abilities { get; private set; }

    public Race(string name, List<Modifier> modifiers, List<Ability> abilities)
    {
        this.name = name;
        this.modifiers = modifiers;
        this.abilities = abilities;
    }
}
