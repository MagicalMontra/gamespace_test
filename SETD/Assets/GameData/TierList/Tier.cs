[System.Serializable]
public class Tier
{
    public TierEnum tier;
    public string tierDesc;
}

[System.Serializable]
public enum TierEnum
{
    Common, Uncommon, Rare, Mystic, Ungodly
}
