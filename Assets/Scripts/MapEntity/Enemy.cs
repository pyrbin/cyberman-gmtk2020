using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MapEntity
{
    public int MaxHealth = 3;
    public int Health
    {
        get { return innerHealth; }
        set { innerHealth = math.min(value, MaxHealth); }
    }
    private int innerHealth;

    void Start()
    {
        Health = MaxHealth;
    }

    public void Damage(int val)
    {
        Health -= val;
        CinemachineShake.ShakeCamera(3f);
        if (IsDead())
        {
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        gameObject.SetActive(false);
        Destroy(this, 0f);
    }

    public bool IsDead() { return Health <= 0; }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!GetPlayer(other.gameObject, out var player)) return;

        if (math.abs(other.relativeVelocity.y) > 0)
        {
            Damage(MaxHealth);
            player
                .GetComponent<Rigidbody2D>()
                .AddForce(new float2(0f, player.GetComponent<CharacterController>().JumpImpulse), ForceMode2D.Impulse);
        }
        else
        {
            player.PauseMovement(0.33f);
            player
                .GetComponent<Rigidbody2D>()
                .AddForce(new float2(-2f, 0f), ForceMode2D.Impulse);
        }
    }
}
