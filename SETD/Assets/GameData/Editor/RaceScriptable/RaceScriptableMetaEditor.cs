using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PlayfabServices.Admin;


[CustomEditor(typeof(RaceScriptableMeta))]
public class RaceScriptableMetaEditor : Editor
{
    List<bool> foldout = new List<bool>();
    List<RaceModifierScriptable> newModifiers = new List<RaceModifierScriptable>();
    List<RaceAbilityScriptable> newAbilities = new List<RaceAbilityScriptable>();
    RaceScriptableMeta dataScriptable;
    Race newRace;

    private void OnEnable()
    {
        dataScriptable = (RaceScriptableMeta)target;
        newRace = new Race();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();

        if (!dataScriptable || dataScriptable.races == null)
            return;

        var dataCount = dataScriptable.races.Count;
        EditorGUILayout.Space();

        if (dataCount <= 0)
            EditorGUI.BeginDisabledGroup(true);

        if (GUILayout.Button("Upload to Playfab"))
        {
            var data = PlayfabContentUpload.SetupData(dataScriptable.races);
            PlayfabContentUpload.UploadGameData("Races", data);
        }

        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        DrawCreatePanel();

        if (dataCount <= 0)
            return;

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Total Race: ", dataCount.ToString());

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
            var race = dataScriptable.races[i];
            foldout[i] = EditorGUILayout.Foldout(foldout[i], race.name);

            if (foldout[i])
            {
                EditorGUI.indentLevel++;
                DrawRaceData(race);
                EditorGUI.indentLevel--;
            }
        }

    }

    private void DrawCreatePanel()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Create New Race", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        GUILayout.Label("Race Name");
        newRace.name = EditorGUILayout.TextArea(newRace.name, GUILayout.Width(300));
        newRace.id = dataScriptable.races.Count;

        GUILayout.Label("Race Modifiers");
        for (int i = 0; i < newModifiers.Count; i++)
        {
            newModifiers[i] = (RaceModifierScriptable)EditorGUILayout.ObjectField(newModifiers[i], typeof(RaceModifierScriptable), false);
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

        GUILayout.Label("Race Abilities");
        for (int i = 0; i < newAbilities.Count; i++)
        {
            newAbilities[i] = (RaceAbilityScriptable)EditorGUILayout.ObjectField(newAbilities[i], typeof(RaceAbilityScriptable), false);
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

        if (string.IsNullOrEmpty(newRace.name) || (newAbilities.Count > 0 && newAbilities.Exists(ability => ability == null))
        || (newModifiers.Count > 0 && newModifiers.Exists(modifier => modifier == null)))
            EditorGUI.BeginDisabledGroup(true);

        if (GUILayout.Button("Create"))
        {
            for (int i = 0; i < newModifiers.Count; i++)
            {
                newRace.modifiers.Add(newModifiers[i].modifier);
            }

            dataScriptable.AddRace(newRace);
            EditorUtility.SetDirty(dataScriptable);
            newRace = new Race();
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
                    dataScriptable.races = dataScriptable.races.OrderBy(method.GetValue).ToList();
                }
            }
            else if (method.Name == "name")
            {
                if (GUILayout.Button("Sort by Name"))
                {
                    dataScriptable.races = dataScriptable.races.OrderBy(method.GetValue).ToList();
                }
            }
            else if (method.Name == "modifiers")
            {
                if (GUILayout.Button("Sort by Modifier Amount"))
                {
                    dataScriptable.races = dataScriptable.races.OrderByDescending(method.GetValue).ToList();
                }
            }
            else if (method.Name == "abilities")
            {
                if (GUILayout.Button("Sort by Ability Amount"))
                {
                    dataScriptable.races = dataScriptable.races.OrderByDescending(method.GetValue).ToList();
                }
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawRaceData(Race race)
    {
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.Space();

        var modifierScriptable = Resources.FindObjectsOfTypeAll<RaceModifierScriptable>().ToList();

        foreach (var method in race.GetType().GetFields())
        {
            if (method.Name == "id")
            {
                EditorGUILayout.LabelField(method.Name, method.GetValue(race).ToString());
            }
            else if (method.Name == "name")
            {
                EditorGUILayout.LabelField("Class Name", EditorStyles.boldLabel);
                race.name = EditorGUILayout.TextField(race.name, GUILayout.Width(300));
            }
            else if (method.Name == "modifiers")
            {
                for (int i = 0; i < race.modifiers.Count; i++)
                {
                    var scriptable = modifierScriptable.Find(m => m.modifier.modName == race.modifiers[i].modName);
                    var modifier = scriptable.modifier;
                    EditorGUILayout.LabelField("Mod #" + i + " " + modifier.modName, EditorStyles.boldLabel);
                    EditorGUILayout.ObjectField(scriptable, typeof(RaceModifierScriptable), false);
                    EditorGUILayout.LabelField("Target Stat");
                    modifier.targetStat = (TargetStat)EditorGUILayout.EnumPopup(modifier.targetStat);
                    EditorGUILayout.LabelField("Mod Type: " + modifier.modType.ToString());
                    if (scriptable.modifier.modType == ModType.Percentage)
                    {
                        string percentage = modifier.modAmount.ToString("#%");
                        EditorGUILayout.LabelField(percentage);
                        modifier.modAmount = EditorGUILayout.Slider(modifier.modAmount, 0.01f, 2);
                        modifier.modDesc = "Increase " + modifier.targetStat.ToString() + " By " + modifier.modAmount.ToString("#%");
                    }
                    else
                    {
                        EditorGUILayout.LabelField("Add Amount");
                        modifier.modAmount = EditorGUILayout.FloatField(modifier.modAmount);
                        modifier.modDesc = "Add " + modifier.modAmount.ToString() + " " + modifier.targetStat.ToString();
                    }
                }
            }
            else if (method.Name == "abilities")
            {
                EditorGUILayout.LabelField("Ability Amount", race.abilities.Count.ToString());
            }
            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Delete"))
        {
            dataScriptable.RemoveRace(race);
            EditorUtility.SetDirty(dataScriptable);
        }

        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }
}
