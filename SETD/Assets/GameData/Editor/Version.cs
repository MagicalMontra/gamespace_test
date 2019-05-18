using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Version
{
    public int mainVersion;
    public int subVersion;
    public int balancedVersion;

    public string GetVersion()
    {
        string mVersion = (mainVersion <= 10) ? "0" + mainVersion : mainVersion.ToString();
        string sVersion = (subVersion <= 10) ? "0" + subVersion : subVersion.ToString();
        string bVersion = (balancedVersion <= 10) ? "0" + balancedVersion : balancedVersion.ToString();
        return mVersion + "." + sVersion + "." + bVersion;
    }
}
