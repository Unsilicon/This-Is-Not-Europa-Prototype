using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapSeed;

    public int mapWidth;

    public int mapHeight;

    public float mapScale;

    public Transform player;

    public Rock limestone;

    public float limestoneProbability;

    public Ore ironOre;

    public float ironOreScale;

    public float ironOreProbability;

    private Rock[,] map;

    private void Awake()
    {
        map = new Rock[mapWidth, mapHeight];
    }

    private void Start()
    {
        InitSeed();
        GenerateBase();
        GenerateOre(ironOre);
        InstantiateMap();
        InitPlayerSpawnPoint();
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
        float xOffset = Random.Range(0, 1000f);
        float yOffset = Random.Range(0, 1000f);
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float noise = Mathf.PerlinNoise(x / mapScale + xOffset, y / mapScale + yOffset);
                if (noise <= limestoneProbability)
                {
                    Rock runtimeRock = limestone.Clone();
                    map[x, y] = runtimeRock;
                }
            }
        }
    }

    // 在地形中生成矿脉
    private void GenerateOre(Ore ore)
    {
        float xOffset = Random.Range(0, 1000f);
        float yOffset = Random.Range(0, 1000f);
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float noise = Mathf.PerlinNoise(x / ironOreScale + xOffset, y / ironOreScale + yOffset);
                if (map[x, y] && noise <= ironOreProbability)
                {
                    Ore runtimeOre = ore.Clone();
                    map[x, y].ore = runtimeOre;
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
                    Transform rock = Instantiate(map[x, y].prefab, new(x, y), Quaternion.identity, transform).transform;
                    if (map[x, y].ore && map[x, y].ore.type != OreType.None)
                    {
                        Instantiate(map[x, y].ore.prefab, rock);
                    }
                }
            }
        }
    }

    // 初始化玩家的出生点
    private void InitPlayerSpawnPoint()
    {
        for (int x = mapWidth / 4; x < mapWidth - mapWidth / 4; x++)
        {
            for (int y = mapHeight / 4; y < mapHeight - mapHeight / 4; y++)
            {
                // 为玩家选择一个周围空旷的出生点
                bool isEmpty = true;
                if (!map[x, y])
                {
                    for (int nearX = x - 1; nearX <= x + 1; nearX++)
                    {
                        for (int nearY = y - 1; nearY <= y + 1; nearY++)
                        {
                            if (map[nearX, nearY])
                            {
                                isEmpty = false;
                                break;
                            }
                        }
                        if (!isEmpty)
                        {
                            break;
                        }
                    }
                    if (isEmpty)
                    {
                        player.position = new(x, y);
                        return;
                    }
                }
            }
        }
    }
}
