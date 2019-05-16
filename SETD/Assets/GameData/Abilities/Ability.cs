using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
    public string abName;
    public string abDesc;
    public int cost;
    public float abCooldown;
    public Tier abTier;

    float cdTimer;
    bool isReady = true;

    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }

    protected IEnumerator CooldownCounter()
    {
        isReady = false;

        while (cdTimer > 0)
        {
            cdTimer -= 0.1f;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        isReady = true;
    }
}
