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

        if (spawnedDic.TryGetValue(xSpawnPos, out var _))
            return;

        var entt = PickRandom();
        float r = Random.Range(0f, 1f);
        if (r < chanceOfObstacle)
        {
            Vector2Int? topTilePos = getTopTile(xSpawnPos, tilemap, map);
            if (topTilePos != null)
            {
                int start = Mathf.FloorToInt(entt.Config.SpawnHeight.x);
                int end = Mathf.FloorToInt(entt.Config.SpawnHeight.y);

                int spawnPoint = Random.Range(start, end);

                Grid layout = tilemap.layoutGrid;
                // Spawn any object
                var tmp = Instantiate(entt.gameObject, obstacleHolder);
                tmp.transform.localPosition = layout.CellToWorld(new Vector3Int(topTilePos.Value.x, topTilePos.Value.y + 1 + spawnPoint, 0));
                tmp.GetComponent<MapEntity>().OnSpawn(GetComponent<LevelGenerator>());

            }
        }

        spawnedDic.Add(xSpawnPos, true);
    }

    public void DespawnObstacles(int xPlayerPos)
    {
        int allowedRange = xPlayerPos - despawnRange;
        foreach (Transform child in obstacleHolder)
            if (child.transform.position.x < allowedRange)
                Object.DestroyImmediate(child.gameObject);

    }

    public MapEntity PickRandom()
    {
        return mapEntities[UnityEngine.Random.Range(0, mapEntities.Count)];
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
