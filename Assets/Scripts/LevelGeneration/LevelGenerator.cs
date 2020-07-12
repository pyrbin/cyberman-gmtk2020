using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

using UnityEngine.Tilemaps;

//[ExecuteInEditMode]
[RequireComponent(typeof(MapSettings), typeof(MapEntitySpawner))]
public class LevelGenerator : MonoBehaviour
{

    public Tilemap tilemap;
    public Tile tile;
    public Tile topTile;

    [MinMaxSlider(-100, 100)]
    public Vector2 renderRange;

    public bool renderAll = false;

    public Transform player;

    public MapSettings mapSettings;

    public MapEntitySpawner mapEntitySpawner;

    private bool generatedMap = false;

    private int[,] map;

    public Transform winItem;


    void Awake()
    {
        GenerateMap();
    }

    void Update()
    {

        if (generatedMap && map != null && tilemap != null)
        {
            RenderMap(map, tilemap, tile);
            mapEntitySpawner.SpawnObstacles(Mathf.FloorToInt(player.position.x), tilemap, map);
            mapEntitySpawner.DespawnObstacles(Mathf.FloorToInt(player.position.x));
        }
    }


    [Button("Generate map")]
    public void GenerateMap()
    {
        ClearMap();
        map = mapSettings.GenerateArray();
        mapSettings.ApplySettings(ref map);
        tilemap.transform.localPosition = new float3(0f, -mapSettings.height, 0f);
        SpawnWinItem();
        generatedMap = true;
    }


    public void SpawnWinItem()
    {
        for (int x = map.GetUpperBound(0); x >= 0; x--)
        {
            for (int y = map.GetUpperBound(1); y >= 0; y--)
            {
                if (map[x, y] == 1)
                {
                    winItem.transform.localPosition = new float3(x, y, 0f);
                    return;
                }
            }
        }
    }

    public void ClearMap()
    {
        tilemap.ClearAllTiles();
        generatedMap = false;
    }


    /// <summary>
    /// Draws the map to the screen
    /// </summary>
    /// <param name="map">Map that we want to draw</param>
    /// <param name="tilemap">Tilemap we will draw onto</param>
    /// <param name="tile">Tile we will draw with</param>
    public void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
    {
        tilemap.ClearAllTiles(); //Clear the map (ensures we dont overlap)

        int startRender = Mathf.FloorToInt(player.position.x + renderRange.x);
        int endRender = Mathf.FloorToInt(player.position.x + renderRange.y);

        // Dont render beyond the edge of the map
        if (endRender > map.GetUpperBound(0))
            endRender = map.GetUpperBound(0);

        // Dont render beyond the edge of the map
        if (startRender < 0)
            startRender = 0;


        if (renderAll)
        {
            startRender = 0;
            endRender = endRender + 500;
            // Dont render beyond the edge of the map
            if (endRender > map.GetUpperBound(0))
                endRender = map.GetUpperBound(0);
        }


        for (int x = startRender; x < endRender; x++) //Loop through the width of the map
        {
            bool topLayer = true;
            for (int y = map.GetUpperBound(1); y >= 0; y--) //Loop through the height of the map
            {
                if (map[x, y] == 1) // 1 = tile, 0 = no tile
                {
                    if (topLayer)
                    {
                        topLayer = false;
                        tilemap.SetTile(new Vector3Int(x, y, 0), topTile);
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                    }
                }
            }
            topLayer = false;
        }
    }





    /*
        public void Apply(ref int[,] map, float seed)
        {
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                Vector2Int? topTile = getTopTile(x);
                if (topTile != null)
                {
                    float r = Random.Range(0f, 1f);
                    if (r < chanceOfObstacle)
                    {
                        int start = Mathf.FloorToInt(spawnDistanceFromGround.x);
                        int end = Mathf.FloorToInt(spawnDistanceFromGround.y);
                        int spawnPoint = Random.Range(start, end);

                        // Spawn any object
                        GameObject tmp = Instantiate(Object);
                        tmp.transform.position = new Vector3(topTile.Value.x, topTile.Value.y + spawnPoint, 0.0f);
                        //map[topTile.Value.x, topTile.Value.y + spawnPoint] = 1;
                    }
                }
            }
        }*/




}
