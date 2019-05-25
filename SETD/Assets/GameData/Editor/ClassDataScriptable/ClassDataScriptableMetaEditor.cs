using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ClassDataScriptableMeta))]
public class ClassDataScriptableMetaEditor : Editor
{
    List<bool> foldout = new List<bool>();
    ClassDataScriptableMeta dataScriptable;
    ClassData newClass;

    List<ClassModifierScriptable> newModifiers = new List<ClassModifierScriptable>();
    List<ClassAbilityScriptable> newAbilities = new List<ClassAbilityScriptable>();

    private void OnEnable()
    {
        dataScriptable = (ClassDataScriptableMeta)target;
        newClass = new ClassData();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();

        if (!dataScriptable || dataScriptable.classes == null)
            return;

        var dataCount = dataScriptable.classes.Count;

        EditorGUILayout.LabelField("Catalogs Version", EditorStyles.boldLabel);
        EditorGUILayout.LabelField(dataScriptable.tableVersion.GetVersion(), EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (dataCount <= 0)
            EditorGUI.BeginDisabledGroup(true);

        if (GUILayout.Button("Upload to Playfab"))
        {
            dataScriptable.tableVersion.subVersion++;
        }

        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        DrawCreatePanel();

        if (dataCount <= 0)
            return;

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Total Class: ", dataCount.ToString());

        EditorGUILayout.Space();
        DrawSortButtons();
        EditorGUILayout.Space();


        while (foldout.Count < dataCount)
        {
            foldout.Add(false);
        }
        while (foldout.Count > dataCount)
        {
            foldout.RemoveAt(foldout.Count - 1);
        }

        for (int i = 0; i < dataCount; i++)
        {
            var job = dataScriptable.classes[i];
            foldout[i] = EditorGUILayout.Foldout(foldout[i], job.name);

            if (foldout[i])
            {
                EditorGUI.indentLevel++;
                DrawClassData(job);
                EditorGUI.indentLevel--;
            }
        }

    }

    private void DrawCreatePanel()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Create New Class", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        GUILayout.Label("Class Name");
        newClass.name = EditorGUILayout.TextArea(newClass.name, GUILayout.Width(300));
        newClass.id = dataScriptable.classes.Count;

        GUILayout.Label("Class Modifiers");
        for (int i = 0; i < newModifiers.Count; i++)
        {
            newModifiers[i] = (ClassModifierScriptable)EditorGUILayout.ObjectField(newModifiers[i], typeof(ClassModifierScriptable), false);
            if (GUILayout.Button("Remove Modifier"))
            {
                newModifiers.RemoveAt(i);
            }
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Add Modifier"))
        {
            newModifiers.Add(null);
        }
        EditorGUILayout.Space();

        GUILayout.Label("Class Abilities");
        for (int i = 0; i < newAbilities.Count; i++)
        {
            newAbilities[i] = (ClassAbilityScriptable)EditorGUILayout.ObjectField(newAbilities[i], typeof(ClassAbilityScriptable), false);
            if (GUILayout.Button("Remove Ability"))
            {
                newAbilities.RemoveAt(i);
            }
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Add Ability"))
        {
            newAbilities.Add(null);
        }
        EditorGUILayout.Space();

        if (string.IsNullOrEmpty(newClass.name) || (newAbilities.Count > 0 && newAbilities.Exists(ability => ability == null))
        || (newModifiers.Count > 0 && newModifiers.Exists(modifier => modifier == null)))
            EditorGUI.BeginDisabledGroup(true);

        if (GUILayout.Button("Create"))
        {
            dataScriptable.AddClass(newClass);
            EditorUtility.SetDirty(dataScriptable);
            newClass = new ClassData();
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    private void DrawSortButtons()
    {
        EditorGUILayout.BeginHorizontal();
        foreach (var method in typeof(ClassData).GetFields())
        {
            if (method.Name == "id")
            {
                if (GUILayout.Button("Sort by Class ID"))
                {
                    dataScriptable.classes = dataScriptable.classes.OrderBy(method.GetValue).ToList();
                }
            }
            else if (method.Name == "name")
            {
                if (GUILayout.Button("Sort by Name"))
                {
                    dataScriptable.classes = dataScriptable.classes.OrderBy(method.GetValue).ToList();
                }
            }
            else if (method.Name == "modifiers")
            {
                if (GUILayout.Button("Sort by Modifier Amount"))
                {
                    dataScriptable.classes = dataScriptable.classes.OrderByDescending(method.GetValue).ToList();
                }
            }
            else if (method.Name == "abilities")
            {
                if (GUILayout.Button("Sort by Ability Amount"))
                {
                    dataScriptable.classes = dataScriptable.classes.OrderByDescending(method.GetValue).ToList();
                }
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawClassData(ClassData job)
    {
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.Space();

        foreach (var method in job.GetType().GetFields())
        {
            if (method.Name == "id")
            {
                EditorGUILayout.LabelField(method.Name, method.GetValue(job).ToString());
            }
            else if (method.Name == "name")
            {
                EditorGUILayout.LabelField(method.Name, method.GetValue(job).ToString());
            }
            else if (method.Name == "modifiers")
            {
                EditorGUILayout.LabelField("Modifier Amount", job.modifiers.Count.ToString());
            }
            else if (method.Name == "abilities")
            {
                EditorGUILayout.LabelField("Ability Amount", job.abilities.Count.ToString());
            }
            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Delete"))
        {
            dataScriptable.DeleteClass(job);
            EditorUtility.SetDirty(dataScriptable);
        }

        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }

    private void UploadItemTable()
    {
        // Playfab calling here.
    }
}
