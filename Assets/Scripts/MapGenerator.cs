using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapSeed;

    public int mapWidth;

    public int mapHeight;

    public float mapScale;

    public Mineral limestone;

    public float limestoneProbability;

    private Mineral[,] map;

    private void Awake()
    {
        map = new Mineral[mapWidth, mapHeight];
    }

    private void Start()
    {
        InitSeed();
        GenerateBase();
        InstantiateMap();
    }

    // 初始化地图种子
    private void InitSeed()
    {
        if (mapSeed == 0)
        {
            mapSeed = Random.Range(int.MinValue, int.MaxValue);
        }
        Random.InitState(mapSeed);
    }


    // 生成基础地形的结构
    private void GenerateBase()
    {
        float xOffset = Random.Range(0, 1000);
        float yOffset = Random.Range(0, 1000);
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float noise = Mathf.PerlinNoise(x / mapScale + xOffset, y / mapScale + yOffset);
                if (noise <= limestoneProbability)
                {
                    map[x, y] = limestone;
                }
            }
        }
    }

    // 实例化地图内容
    private void InstantiateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (map[x, y])
                {
                    Instantiate(map[x, y].prefab, new(x, y), Quaternion.identity, transform);
                }
            }
        }
    }
}
