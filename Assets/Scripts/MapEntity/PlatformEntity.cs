using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class PlatformEntity : MapEntity
{
    public int MaxLength;

    public int MinLength;

    public GameObject Tile;

    [Range(0f, 1f)]
    public float Chance;

    private int length;

    public override void OnSpawn(LevelGenerator generator)
    {
        length = UnityEngine.Random.Range(MinLength, MaxLength);

        var entt = generator.GetComponent<MapEntitySpawner>().PickRandom();

        while (entt.GetType() == this.GetType() && generator.GetComponent<MapEntitySpawner>().mapEntities.Count > 1)
        {
            entt = generator.GetComponent<MapEntitySpawner>().PickRandom();
        }

        for (int i = 0; i < length; i++)
        {
            var tile = Instantiate(Tile, this.transform);
            tile.transform.localPosition = new float3(i, 0, 0);
            tile.GetComponent<SpriteRenderer>().sprite = generator.topTile.sprite;

            if (UnityEngine.Random.Range(0f, 1f) <= Chance && (entt.GetType() != this.GetType()))
            {
                var go = Instantiate(entt, tile.transform);
                go.transform.localPosition = new float3(0, 1, 0);
            }
        }
    }



}
