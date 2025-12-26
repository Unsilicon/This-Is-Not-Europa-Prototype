using UnityEngine;

[CreateAssetMenu(fileName = "Ore", menuName = "Scriptable Objects/Ore")]
public class Ore : ScriptableObject
{
    public OreType type;

    public GameObject prefab;

    public Ore Clone()
    {
        Ore ore = CreateInstance<Ore>();
        ore.type = type;
        ore.prefab = prefab;
        return ore;
    }
}

public enum OreType
{
    None,
    IronOre
}
