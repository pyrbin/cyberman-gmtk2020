﻿using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(DynamicGravity))]
public class CharacterController : MonoBehaviour
{
    public float Speed = 2f;
    public float JumpImpulse = 7f;

    public ContactFilter2D ContactFilter;

    public float? SpeedMod { get; set; } = null;
    public bool IsSliding { get; private set; }

    private Rigidbody2D rbody;
    private Collider2D coll;


    public void Jump(float mod = 1f)
    {
        if (!IsGrounded) return;
        rbody.AddForce(Vector2.up * JumpImpulse * mod, ForceMode2D.Impulse);
    }

    public void ToggleSlide(bool hover = false)
    {
        if (!IsSliding)
        {
            coll.transform.localScale = new float3(1f, hover ? 0 : 0.5f, 1f);
            coll.transform.localPosition = new float3(0, hover ? 0 : -0.75f, 0f);
            if (hover)
            {
                rbody.velocity = new float2(rbody.velocity.x, 0f);
                GetComponent<DynamicGravity>().OverrideGravity = 0f;
            }
        }
        else
        {
            coll.transform.localScale = new float3(1f);
            coll.transform.localPosition = new float3(0f);
            GetComponent<DynamicGravity>().OverrideGravity = null;
        }

        IsSliding = !IsSliding;
    }

    // We can check to see if there are any contacts given our contact filter
    // which can be set to a specific layer and normal angle.
    private bool IsGrounded => rbody.IsTouching(ContactFilter);

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        coll = GetComponentInChildren<Collider2D>();
    }

    void FixedUpdate()
    {
        // Set sideways velocity.
        rbody.velocity = new float2(Speed * SpeedMod ?? 1f, rbody.velocity.y);
    }
}