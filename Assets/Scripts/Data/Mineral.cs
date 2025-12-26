using UnityEngine;

[CreateAssetMenu(fileName = "Mineral", menuName = "Scriptable Objects/Mineral")]
public class Mineral : ScriptableObject
{
    public MineralType type;

    public GameObject prefab;
}

public enum MineralType
{
    Limestone,
}