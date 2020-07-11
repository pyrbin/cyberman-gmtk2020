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


    private int[,] map;

    private MapSettings mapSettings;

    // Start is called before the first frame update
    void Awake()
    {
        mapSettings = GetComponent<MapSettings>();
        GenerateMap();
    }

    [Button("Generate map")]
    public void GenerateMap()
    {
        ClearMap();
        var map = mapSettings.GenerateArray();
        mapSettings.ApplySettings(ref map);
        RenderMap(map, tilemap, tile);
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
        for (int x = 0; x < map.GetUpperBound(0); x++) //Loop through the width of the map
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
