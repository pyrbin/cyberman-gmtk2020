using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NaughtyAttributes;
using System.Runtime.CompilerServices;

public class MapSettings : MonoBehaviour
{
    public float seed;

    public int width;

    public int height;

    [ReorderableList]
    public List<MapFunction> mapFunctions;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ApplySettings(ref int[,] map)
    {
        foreach (MapFunction function in mapFunctions)
        {
            function.Apply(ref map, seed);
        }
    }

    public int[,] GenerateArray()
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                map[x, y] = 0;
            }
        }
        return map;
    }

}
