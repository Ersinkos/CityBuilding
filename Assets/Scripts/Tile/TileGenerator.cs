using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public static TileGenerator instance;
    [SerializeField] GameObject TilePrefab;
    public TileTypeListSO tileTypeList;
    public int mapSize;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GenerateTiles();
        BuildManager.instance.BuildHqOnGameStart();
    }
    private void GenerateTiles()
    {
        for (int i = -mapSize; i <= mapSize; i += 2)
        {
            for (int j = -mapSize; j <= mapSize; j += 2)
            {
                Instantiate(TilePrefab, new Vector3((float)i, 0.007f, (float)j), Quaternion.identity);
            }
        }
    }
    public TileTypeListSO GetTileTypeList()
    {
        return tileTypeList;
    }
    public TileSO GetRandomTileType()
    {
        return tileTypeList.list[Random.Range(0, tileTypeList.list.Count)];
    }

}
