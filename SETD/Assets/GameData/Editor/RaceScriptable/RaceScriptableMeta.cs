using System.Xml.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RaceScriptableMeta", menuName = "Race/Create Scriptable")]
[Serializable]
public class RaceScriptableMeta : ScriptableObject
{
    public List<Race> races = new List<Race>();

    public void AddRace(Race race)
    {
        races.Add(race);
    }

    public void RemoveRace(Race race)
    {
        races.Remove(race);
    }
}
