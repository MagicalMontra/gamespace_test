using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Race
{
    public string name { get; private set; }
    public RaceModifier modifier { get; private set; }
    public List<Ability> abilities { get; private set; }

    public Race(string name, RaceModifier modifier, List<Ability> abilities)
    {
        this.name = name;
        this.modifier = modifier;
        this.abilities = abilities;
    }
}
