using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AddObstacles : MapFunction
{

    public float seed;
    public float chanceOfObstacle;

    [MinMaxSlider(0, 10)]
    public Vector2 spawnDistanceFromGround;

    public TileBase tile;

    public override void Apply(ref int[,] map, float seed)
    {
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            Vector2Int? topTile = getTopTile(map, x);
            if (topTile != null)
            {
                float r = Random.Range(0f, 1f);
                if (r < chanceOfObstacle)
                {
                    int start = Mathf.FloorToInt(spawnDistanceFromGround.x);
                    int end = Mathf.FloorToInt(spawnDistanceFromGround.y);

                    int spawnPoint = Random.Range(start, end);
                    map[topTile.Value.x, topTile.Value.y + spawnPoint] = 1;
                }
            }
        }
    }

    private Vector2Int? getTopTile(int[,] map, int x)
    {
        for (int y = map.GetUpperBound(1); y > 0; y--)
        {
            if (map[x, y] == 1)
            {
                Debug.Log("x: " + x + ", y: " + y);
                return new Vector2Int(x, y);
            }
        }
        return null;
    }
}