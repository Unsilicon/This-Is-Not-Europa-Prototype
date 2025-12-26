using UnityEngine;

[CreateAssetMenu(fileName = "Rock", menuName = "Scriptable Objects/Rock")]
public class Rock : ScriptableObject
{
    public RockType type;

    public GameObject prefab;

    public Ore ore;

    public Rock Clone()
    {
        Rock rock = CreateInstance<Rock>();
        rock.type = type;
        rock.prefab = prefab;
        rock.ore = ore;
        return rock;
    }
}
public enum RockType
{
    Limestone
}
