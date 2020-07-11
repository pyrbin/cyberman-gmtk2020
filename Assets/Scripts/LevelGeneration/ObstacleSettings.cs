using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSettings : MonoBehaviour
{

    private Dictionary<int, bool> spawnedDic = new Dictionary<int, bool>();

    public Transform obstacleHolder;
    public float chanceOfObstacle;

    [MinMaxSlider(0, 10)]
    public Vector2 spawnDistanceFromGround;

    public GameObject Object;


    public void SpawnObstacles(int xSpawnPos, Tilemap tilemap, int[,] map)
    {
        bool ignoreMe;
        if (spawnedDic.TryGetValue(xSpawnPos, out ignoreMe))
            return;

        float r = Random.Range(0f, 1f);
        if (r < chanceOfObstacle)
        {
            Vector2Int? topTilePos = getTopTile(xSpawnPos, tilemap, map);
            if (topTilePos != null)
            {
                int start = Mathf.FloorToInt(spawnDistanceFromGround.x);
                int end = Mathf.FloorToInt(spawnDistanceFromGround.y);
                int spawnPoint = Random.Range(start, end);

                Grid layout = tilemap.layoutGrid;
                // Spawn any object
                GameObject tmp = Instantiate(Object, obstacleHolder);
                tmp.transform.position = layout.CellToWorld(new Vector3Int(topTilePos.Value.x, topTilePos.Value.y + 1 + spawnPoint, 0));
                spawnedDic.Add(xSpawnPos, true);
            }
        }
    }

    public void DespawnObstacles()
    {

    }

    private Vector2Int? getTopTile(int x, Tilemap tilemap, int[,] map)
    {
        for (int y = map.GetUpperBound(1); y > 0; y--)
        {
            if (tilemap.HasTile(new Vector3Int(x, y, 0)))
            {
                return new Vector2Int(x, y);
            }
        }
        return null;
    }

}