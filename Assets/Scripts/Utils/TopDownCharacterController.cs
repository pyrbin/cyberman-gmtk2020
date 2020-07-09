using Unity.Mathematics;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D), typeof(Collider2D))]
public class TopDownCharacterController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] Transform Model = null;
    [SerializeField] Animator Animator = null;

    [Header("Movement")]
    [SerializeField] float speed = 0.0f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    [SerializeField] float diagonalMod = 0.7f;

    [Header("Look")]
    [SerializeField] bool lookAtCursor = false;

    private PlayerInput input;
    private Rigidbody2D rb;
    private Collider2D co;

    private bool isFlipped;

    private int animatorRunningBool;
    // private int animatorGroundedBool;
    // private int animatorJumpTrigger;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        co = GetComponent<Collider2D>();

        rb.gravityScale = 0;

        animatorRunningBool = Animator.StringToHash("Running");
        // animatorGroundedBool = Animator.StringToHash("Grounded");
        // animatorJumpTrigger = Animator.StringToHash("Jump");
    }

    void FixedUpdate()
    {
        Move(input.MovementInput);
        CheckFlip();
    }

    private void Move(float2 val)
    {
        // Move the character by finding the target velocity
        // And then smoothing it out and applying it to the character
        var tmp = Vector2.zero;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, (Time.deltaTime * speed * val), ref tmp, movementSmoothing);
        Animator.SetBool(animatorRunningBool, math.length(math.abs(val)) > 0);
    }

    private void CheckFlip()
    {
        if (lookAtCursor)
        {
            if ((transform.position.x > input.MousePosition.x && !isFlipped)
                || (transform.position.x < input.MousePosition.x && isFlipped))
            {
                Flip();
            }
        }
        else if ((input.MovementInput.x > 0 && isFlipped)
                || (input.MovementInput.x < 0 && !isFlipped))
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFlipped = !isFlipped;
        // Multiply the player's x local scale by -1.
        var tmp = transform.localScale;
        tmp.x *= -1;
        transform.localScale = tmp;
    }
}
