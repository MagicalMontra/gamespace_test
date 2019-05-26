using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifierScriptableMeta", menuName = "Modifier Data/Create Scriptable")]
[Serializable]
public class ModifierScriptableMeta : ScriptableObject
{
    public List<ModifierScriptable> modifiers;

    public void AddMod(ModifierScriptable modToCreate)
    {
        modifiers.Add(modToCreate);
    }

    public void DeleteMod(ModifierScriptable modToDelete)
    {
        modifiers.Remove(modToDelete);
    }
}
