using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings.SplashScreen;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpHeight = 1000.0f;
    [SerializeField] private float gravity = -9.8f;
    
    [Header("Movement")]
    [SerializeField] float maxSpeed = 100.0f;
    [SerializeField] float acceleration = 300.0f;
    [SerializeField] float brakingCoefficient = 0.05f;

    [SerializeField]
    private Transform groundCheck;

    private const float groundedRadius = .2f;
    private bool grounded;
    private const float fallingThreshold = 0.0f;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;

    private bool mustJump = false;
    private bool isJumping = false;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        Debug.Log($"Move Input: {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"We are supposed to jump.");

            mustJump = true;
        }
        else
        {
            Debug.Log($"We are supposed to NOT jump.");
        }
    }

    // Update is called once per frame
    // Used for regular updates such as:
    //  * Moving non-physics objects.
    //  * Simple Timers.
    //  * Receiving Inputs.
    //
    // It's not call on a regular time:
    //   If one frame takes longer to be processed, then the time between calls update will be different.
    void Update()
    {
        rb2D.linearVelocityX = moveInput.x * speed;

        if (moveInput.x != 0.0f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (rb2D.linearVelocityY < fallingThreshold)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }

        if (moveInput.x > 0.0f)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0.0f)
        {
            spriteRenderer.flipX = true;
        }

        if (mustJump && IsGrounded())
        {
            Debug.Log("Jumping");

            animator.SetBool("IsJumping", true);
            animator.SetBool("IsWalking", false);

            mustJump = false;
            isJumping = true;

            rb2D.linearVelocityY = jumpHeight;
        }
    }

    // Called every physics step.
    // FixedUpdate intervals are consistent.
    // Used for regular updates such as:
    //  * Adjusting physics (Rigidbody) objetcs.
    private void FixedUpdate()
    {
        if (rb2D.linearVelocityY < fallingThreshold)
        {
            bool wasGrounded = grounded;
            grounded = false;
            Collider2D[] colliders =
                Physics2D.OverlapCircleAll(
                    groundCheck.position,
                    groundedRadius,
                    groundLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    Debug.Log("Not jumping");
                    isJumping = false;

                    animator.SetBool("IsFalling", false);
                    break;
                }
            }
        }        
    }

    public bool IsGrounded()
    {
        return
            Physics2D.BoxCast(
                transform.position,
                boxSize,
                0,
                -transform.up,
                castDistance,
                groundLayer);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
