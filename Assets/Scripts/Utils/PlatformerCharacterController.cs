using Unity.Mathematics;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D), typeof(Collider2D))]
public class PlatformerCharacterController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] Transform Model = null;
    [SerializeField] Animator Animator = null;


    [Header("Movement")]
    [SerializeField] float speed = 0.0f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    [SerializeField] float jumpForce = 0.0f;

    [Header("Look")]
    [SerializeField] bool lookAtCursor = false;

    [Header("Layers")]
    [SerializeField] LayerMask groundMask;

    private PlayerInput input;
    private Rigidbody2D rb;
    private Collider2D co;

    private bool isFlipped;
    private bool isJumping;
    private bool isFalling;
    private bool isGrounded;
    private bool JumpRequest;

    private int animatorRunningBool;
    private int animatorGroundedBool;
    private int animatorJumpTrigger;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        co = GetComponent<Collider2D>();

        if (rb.gravityScale == 0)
            rb.gravityScale = 1f;

        animatorRunningBool = Animator.StringToHash("Running");
        animatorGroundedBool = Animator.StringToHash("Grounded");
        animatorJumpTrigger = Animator.StringToHash("Jump");
    }

    void Update()
    {
        if (!isJumping && input.SpacePressed)
            JumpRequest = true;
    }

    void FixedUpdate()
    {
        CheckGrounded();
        Move(input.MovementInput.x);
        UpdateJump();
        CheckFlip();
    }

    private void CheckGrounded()
    {
        // Use character collider to check if touching ground layers
        var tmp = isGrounded;
        isGrounded = co.IsTouchingLayers(groundMask);
        Animator.SetBool(animatorGroundedBool, isGrounded);

        // Landed
        if (tmp != isGrounded && isGrounded)
            CinemachineShake.ShakeCamera(1, 0.33f);
    }

    private void UpdateJump()
    {
        // Set falling flag
        if (isJumping && rb.velocity.y < 0)
            isFalling = true;

        // Jump
        if (JumpRequest && isGrounded)
        {
            // Jump using impulse force
            rb.AddForce(new float2(0, jumpForce), ForceMode2D.Impulse);

            // Set animator
            Animator.SetTrigger(animatorJumpTrigger);

            // Set jumping flag
            isJumping = true;
            JumpRequest = false;
        }

        // Landed
        else if (isJumping && isFalling && isGrounded)
        {
            // Reset jumping flags
            isJumping = false;
            isFalling = false;
        }
    }

    private void Move(float val)
    {
        // Move the character by finding the target velocity
        // And then smoothing it out and applying it to the character
        var tmp = 0.0f;
        var velX = Mathf.SmoothDamp(rb.velocity.x, (Time.deltaTime * speed * val), ref tmp, movementSmoothing);
        rb.velocity = new float2(velX, rb.velocity.y);
        Animator.SetBool(animatorRunningBool, isGrounded && math.abs(val) > 0);
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
