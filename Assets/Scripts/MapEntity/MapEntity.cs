using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public struct MapEntitySpawnConfig
{
    public float2 SpawnHeight;
    // public ushort Chance;
}

[RequireComponent(typeof(Collider2D))]
public abstract class MapEntity : MonoBehaviour
{
    protected readonly string PlayerTag = "Player";

    [SerializeField]
    public MapEntitySpawnConfig Config;

    [HideInInspector]
    public Collider2D Collider;

    void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }

    public virtual void OnSpawn(LevelGenerator generator) { }

    protected bool GetPlayer(GameObject go, out Player player)
    {
        if (go.CompareTag(PlayerTag))
        {
            player = go.GetComponentInParent<Player>();
            return true;
        }
        else
        {
            player = null;
            return false;
        }
    }

}
