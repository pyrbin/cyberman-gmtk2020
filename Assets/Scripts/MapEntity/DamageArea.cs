using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public enum DamageDirection
{
    Up, Horizontal, All
}

public class DamageArea : MapEntity
{
    [Range(1, 3)]
    public ushort Damage;

    public DamageDirection Dir = DamageDirection.All;

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageFunc(other.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        bool valid = false;
        switch (Dir)
        {
            case DamageDirection.Up:
                valid = (math.abs(other.relativeVelocity.y) > 0);
                break;
            case DamageDirection.Horizontal:
                valid = (math.abs(other.relativeVelocity.y) <= 0.5f);
                break;
            case DamageDirection.All:
                valid = true;
                break;
        }

        if (valid)
            DamageFunc(other.gameObject);
    }

    void DamageFunc(GameObject other)
    {
        if (!GetPlayer(other, out var player)) return;
        player.Damage(Damage);
    }
}