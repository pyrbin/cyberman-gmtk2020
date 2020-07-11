using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{

    public Tilemap tilemap;
    public TileBase tile;
    public TileBase topTile;

    public int width;
    public int height;

    public float seed = 1.212312f;

    public int minWidth = 10;

    private int[,] map;

    [ReorderableList]
    public List<IMapFunction> mapFunctions;


    // Start is called before the first frame update
    void Start()
    {
        ClearMap();
        GenerateMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ClearMap();
            GenerateMap();
        }
    }

    [ExecuteInEditMode]
    public void GenerateMap()
    {
        ClearMap();
        int[,] map = new int[width, height];
        float seed = Time.time;
        map = MapFunctions.GenerateArray(width, height, true);
        map = MapFunctions.RandomWalkTopSmoothed(map, seed, minWidth);
        map = MapFunctions.AddGaps(map, seed, 4, 1, 0.6f);
        Debug.Log(seed);
        //Render the result
        MapFunctions.RenderMap(map, tilemap, tile);
    }

    public void ClearMap()
    {
        tilemap.ClearAllTiles();
    }
}
