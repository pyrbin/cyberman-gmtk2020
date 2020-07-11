using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class MapEntitySpawner : MonoBehaviour
{
    public Transform obstacleHolder;
    public float chanceOfObstacle;


    public List<MapEntity> mapEntities;

    [Range(0, 100)]
    public int spawnRange;

    [Range(0, 100)]
    public int despawnRange;

    private Dictionary<int, bool> spawnedDic = new Dictionary<int, bool>();

    public void SpawnObstacles(int xPlayerPos, Tilemap tilemap, int[,] map)
    {
        int xSpawnPos = xPlayerPos + spawnRange;

        foreach (var mapEntity in mapEntities)
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
                    int start = Mathf.FloorToInt(mapEntity.Config.SpawnHeight.x);
                    int end = Mathf.FloorToInt(mapEntity.Config.SpawnHeight.y);
                    int spawnPoint = Random.Range(start, end);

                    Grid layout = tilemap.layoutGrid;
                    // Spawn any object
                    GameObject tmp = Instantiate(mapEntity.gameObject, obstacleHolder);
                    tmp.transform.position = layout.CellToWorld(new Vector3Int(topTilePos.Value.x, topTilePos.Value.y + 1 + spawnPoint, 0));
                    spawnedDic.Add(xSpawnPos, true);
                }
            }
        }
    }

    public void DespawnObstacles(int xPlayerPos)
    {
        int allowedRange = xPlayerPos - despawnRange;
        foreach (Transform child in obstacleHolder)
            if (child.transform.position.x < allowedRange)
                Object.DestroyImmediate(child.gameObject);

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