using UnityEngine;

public abstract class MapFunction : MonoBehaviour
{
    public abstract void Apply(ref int[,] map, float seed);
}