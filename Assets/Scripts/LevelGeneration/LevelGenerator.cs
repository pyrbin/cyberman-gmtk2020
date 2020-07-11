using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

using UnityEngine.Tilemaps;

[ExecuteInEditMode]
[RequireComponent(typeof(MapSettings))]
public class LevelGenerator : MonoBehaviour
{

    public Tilemap tilemap;
    public TileBase tile;

    [MinMaxSlider(-100, 100)]
    public Vector2 renderRange;

    public bool renderAll = false;

    public Transform player;

    private int[,] map;

    private MapSettings mapSettings;


    void Awake()
    {
        mapSettings = GetComponent<MapSettings>();
        GenerateMap();
    }

    void Update()
    {
        RenderMap(map, tilemap, tile);
    }

    [Button("Generate map")]
    public void GenerateMap()
    {
        ClearMap();
        map = mapSettings.GenerateArray();
        mapSettings.ApplySettings(ref map);
    }

    public void ClearMap()
    {
        tilemap.ClearAllTiles();
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
            for (int y = 0; y < map.GetUpperBound(1); y++) //Loop through the height of the map
            {
                if (map[x, y] == 1) // 1 = tile, 0 = no tile
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }


}
