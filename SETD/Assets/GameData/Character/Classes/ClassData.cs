using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassData
{
    public string name;
    public int id;
    public List<ClassModifier> modifiers = new List<ClassModifier>();
    public List<Ability> abilities = new List<Ability>();

    public ClassData()
    {

    }

    public ClassData(int id, string name, List<ClassModifier> modifiers, List<Ability> abilities)
    {
        this.id = id;
        this.name = name;
        this.modifiers = modifiers;
        this.abilities = abilities;
    }
}
