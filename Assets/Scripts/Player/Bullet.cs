using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 3)]
    public ushort Damage;

    public Rigidbody2D Rbody { get; set; }

    void Awake()
    {
        Rbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageFunc(other.gameObject);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        DamageFunc(other.gameObject);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this);
    }

    void DamageFunc(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.Damage(Damage);
        }
    }
}
