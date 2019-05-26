using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClassDataScriptable", menuName = "Class Data/Create Scriptable")]
[Serializable]
public class ClassDataScriptableMeta : ScriptableObject
{
    public List<ClassData> classes = new List<ClassData>();

    public void AddClass(ClassData classToCreate)
    {
        classes.Add(classToCreate);
    }

    public void DeleteClass(ClassData classToDelete)
    {
        classes.Remove(classToDelete);
    }
}
