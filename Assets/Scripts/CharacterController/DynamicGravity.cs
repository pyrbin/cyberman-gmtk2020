using UnityEngine;

public class DynamicGravity : MonoBehaviour
{
    public float AgainstGravityScale = 1f;
    public float FallGravityScale = 2f;

    public float? OverrideGravity { get; set; } = null;

    private Rigidbody2D rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Calculate our direction relative to the global gravity.
        var direction = Vector2.Dot(rbody.velocity, Physics2D.gravity);

        // Set the gravity scale accordingly.
        rbody.gravityScale = OverrideGravity ?? (direction > 0f ? FallGravityScale : AgainstGravityScale);
    }
}
