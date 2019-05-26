using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassData
{
    public string name;
    public int id;
    public List<Modifier> modifiers = new List<Modifier>();
    public List<Ability> abilities = new List<Ability>();

    public ClassData()
    {

    }

    public ClassData(int id, string name, List<Modifier> modifiers, List<Ability> abilities)
    {
        this.id = id;
        this.name = name;
        this.modifiers = modifiers;
        this.abilities = abilities;
    }
}
