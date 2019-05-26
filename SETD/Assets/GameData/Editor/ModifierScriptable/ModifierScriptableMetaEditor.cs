using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Utility.Scriptable;
using PlayfabServices.Admin;

#if UNITY_EDITOR
[CustomEditor(typeof(ModifierScriptableMeta))]
public class ModifierScriptableMetaEditor : Editor
{
    public enum TargetModifier
    {
        Equipment = 0, Class, Race
    };
    ModifierScriptableMeta dataScriptable;
    Modifier newMod;
    TargetModifier targetModifer;
    string desc = "";
    string nameToSearch = "";

    private void OnEnable()
    {
        dataScriptable = (ModifierScriptableMeta)target;
        newMod = new Modifier();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();

        if (!dataScriptable || dataScriptable.modifiers == null)
            return;

        var dataCount = dataScriptable.modifiers.Count;

        EditorGUILayout.Space();

        if (dataCount <= 0)
            EditorGUI.BeginDisabledGroup(true);

        if (GUILayout.Button("Upload Class Modifier"))
        {
            List<Modifier> modifiers = new List<Modifier>();
            foreach (var scriptable in dataScriptable.modifiers)
            {
                if (scriptable.GetType() == typeof(ClassModifierScriptable))
                    modifiers.Add(scriptable.modifier);
            }

            var datas = PlayfabContentUpload.SetupData(modifiers);
            PlayfabContentUpload.UploadGameData("ClassModifier", datas);
        }

        if (GUILayout.Button("Upload Race Modifier"))
        {
            List<Modifier> modifiers = new List<Modifier>();
            foreach (var scriptable in dataScriptable.modifiers)
            {
                if (scriptable.GetType() == typeof(RaceModifierScriptable))
                    modifiers.Add(scriptable.modifier);
            }

            var datas = PlayfabContentUpload.SetupData(modifiers);
            PlayfabContentUpload.UploadGameData("RaceModifier", datas);
        }



        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        DrawCreatePanel();

        if (dataCount <= 0)
            return;

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Total Item: ", dataCount.ToString());

        DrawSort();

        EditorGUILayout.Space();
        DrawSearch();
        EditorGUILayout.Space();

        // if (string.IsNullOrEmpty(nameToSearch))
        // {
        //     for (int i = 0; i < dataCount; i++)
        //     {
        //         if (deleteFlag)
        //             break;

        //         DrawModData(dataScriptable.modifiers[i]);
        //     }
        // }


    }

    private void DrawCreatePanel()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Create New Mods", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        newMod.id = dataScriptable.modifiers.Count;
        GUILayout.Label("Modifier Name");
        newMod.modName = EditorGUILayout.TextField(newMod.modName, GUILayout.Width(300));
        GUILayout.Label(new GUIContent("Modifier For?", "For example: for class only mod or for equipment mod."));
        targetModifer = (TargetModifier)EditorGUILayout.EnumPopup(targetModifer);
        GUILayout.Label("Modifier Type");
        newMod.modType = (ModType)EditorGUILayout.EnumPopup(newMod.modType);
        GUILayout.Label("Target Stat");
        newMod.targetStat = (TargetStat)EditorGUILayout.EnumPopup(newMod.targetStat);

        bool isValid = true;

        switch (newMod.modType)
        {
            case ModType.Percentage:
                GUILayout.Label("Multiple Percentage");
                GUILayout.Label(newMod.modAmount.ToString("#%"));
                newMod.modAmount = EditorGUILayout.Slider(newMod.modAmount, 0.01f, 2);
                desc = "Increase " + newMod.targetStat.ToString() + " By " + newMod.modAmount.ToString("#%");
                break;
            case ModType.Add:
                GUILayout.Label("Add amount");
                newMod.modAmount = (float)EditorGUILayout.IntField((int)newMod.modAmount, GUILayout.Width(50));
                desc = "Add " + newMod.modAmount.ToString() + " " + newMod.targetStat.ToString();
                isValid = newMod.modAmount > 0;
                break;
        }

        GUILayout.Label("Mod Description");
        desc = EditorGUILayout.TextArea(desc, GUILayout.Width(300), GUILayout.Height(100));
        newMod.modDesc = desc;

        if (string.IsNullOrEmpty(newMod.modName) || !isValid)
            EditorGUI.BeginDisabledGroup(true);

        EditorGUILayout.Space();
        if (GUILayout.Button("Create"))
        {
            switch (targetModifer)
            {
                case TargetModifier.Equipment:
                    ModifierScriptable mod = ScriptableObjectUtility.CreateAsset<ModifierScriptable>(newMod.modName);
                    mod.modifier = newMod;
                    dataScriptable.AddMod(mod);
                    EditorUtility.SetDirty(mod);
                    break;
                case TargetModifier.Class:
                    ClassModifierScriptable classMod = ScriptableObjectUtility.CreateAsset<ClassModifierScriptable>(newMod.modName);
                    classMod.modifier = newMod;
                    dataScriptable.AddMod(classMod);
                    EditorUtility.SetDirty(classMod);
                    break;
                case TargetModifier.Race:
                    RaceModifierScriptable raceMod = ScriptableObjectUtility.CreateAsset<RaceModifierScriptable>(newMod.modName);
                    raceMod.modifier = newMod;
                    dataScriptable.AddMod(raceMod);
                    EditorUtility.SetDirty(raceMod);
                    break;
            }

            EditorUtility.SetDirty(dataScriptable);
            newMod = new Modifier();
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    private void DrawSearch()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();


        if (GUILayout.Button("Delete All (!CAUTION!)"))
        {
            for (int i = dataScriptable.modifiers.Count - 1; i >= 0; i--)
            {
                ScriptableObjectUtility.DeleteAsset(dataScriptable.modifiers[i]);
            }

            dataScriptable.modifiers.Clear();
            EditorUtility.SetDirty(dataScriptable);
        }

        GUILayout.Label("Search");
        nameToSearch = EditorGUILayout.TextField(nameToSearch, GUILayout.Width(150));

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        List<ModifierScriptable> drawers;
        if (!string.IsNullOrEmpty(nameToSearch))
            drawers = dataScriptable.modifiers.FindAll(m => m.modifier.modName.Contains(nameToSearch));
        else
            drawers = dataScriptable.modifiers;

        for (int i = 0; i < drawers.Count; i++)
        {
            DrawModData(drawers[i]);
        }

    }

    private void DrawSort()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();

        if (GUILayout.Button("Class Modifier First"))
        {
            dataScriptable.modifiers = dataScriptable.modifiers.OrderByDescending(m => m is ClassModifierScriptable).ToList();
        }

        if (GUILayout.Button("Equipment Modifier First"))
        {
            dataScriptable.modifiers = dataScriptable.modifiers.OrderBy(m => m is ModifierScriptable).ToList();
        }

        if (GUILayout.Button("Race Modifier First"))
        {
            dataScriptable.modifiers = dataScriptable.modifiers.OrderByDescending(m => m is RaceModifierScriptable).ToList();
        }

        // if (GUILayout.Button("Race Modifier First"))
        // {
        //     dataScriptable.modifiers = dataScriptable.modifiers.OrderBy(m => m is ModifierScriptable).ToList();
        // }

        if (GUILayout.Button("Sort by Name"))
        {
            dataScriptable.modifiers = dataScriptable.modifiers.OrderBy(m => m.modifier.modName).ToList();
        }

        if (GUILayout.Button("Sort by ID"))
        {
            dataScriptable.modifiers = dataScriptable.modifiers.OrderBy(m => m.modifier.id).ToList();
        }

        if (GUILayout.Button("Sort by Type"))
        {
            dataScriptable.modifiers = dataScriptable.modifiers.OrderByDescending(m => m.modifier.modType).ToList();
        }

        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
    }

    private void DrawModData(ModifierScriptable mod)
    {
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.Space();

        EditorGUILayout.ObjectField(mod, typeof(ModifierScriptable), false);
        EditorGUILayout.Space();

        if (GUILayout.Button("Delete"))
        {
            dataScriptable.DeleteMod(mod);
            ScriptableObjectUtility.DeleteAsset(mod);
        }

        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }
}
#endif