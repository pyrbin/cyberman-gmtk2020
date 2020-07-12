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
    public string OnHitSFX = "";
    public bool DontDamageFromTop = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageFunc(other.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (DontDamageFromTop)
        {
            foreach (var cnt in other.contacts)
            {
                if (cnt.normal.y != -1f)
                    continue;
                else
                    return;
            }
        }

        DamageFunc(other.gameObject);
    }

    void DamageFunc(GameObject other)
    {
        if (!GetPlayer(other, out var player)) return;
        if (OnHitSFX != "") FMODUnity.RuntimeManager.PlayOneShot("event:/" + OnHitSFX);
        player.Damage(Damage);
    }
}
