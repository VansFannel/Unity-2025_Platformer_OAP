using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpHeight = 1000.0f;
    //[SerializeField] private float gravity = -9.8f;

    [Header("Movement")]
    //[SerializeField] float maxSpeed = 100.0f;
    //[SerializeField] float acceleration = 300.0f;
    //[SerializeField] float brakingCoefficient = 0.05f;

    [SerializeField]
    private Transform groundCheck;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSoundClip;

    private const float groundedRadius = .2f;
    private const float fallingThreshold = -0.1f;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;

    private bool mustJump = false;
    private bool isJumping = false;

    private AudioSource audioSource;

    private enum AnimatorState
    {
        Idle,
        Walking,
        Jumping,
        Falling
    }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        //Debug.Log($"Move Input: {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log($"We are supposed to jump.");

            mustJump = true;
        }
        else
        {
            //Debug.Log($"We are supposed to NOT jump.");
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
            SetAnimatorState(AnimatorState.Walking);
        }
        else
        {
            SetAnimatorState(AnimatorState.Idle);
        }

        if (rb2D.linearVelocityY < fallingThreshold)
        {
            SetAnimatorState(AnimatorState.Falling);
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
            SetAnimatorState(AnimatorState.Jumping);

            mustJump = false;
            isJumping = true;

            rb2D.linearVelocityY = jumpHeight;

            SoundFXManager.instance.PlaySoundFXClip(jumpSoundClip, transform, 1.0f);
        }
    }

    // Called every physics step.
    // FixedUpdate intervals are consistent.
    // Used for regular updates such as:
    //  * Adjusting physics (Rigidbody) objetcs.
    private void FixedUpdate()
    {
        if (isJumping && (rb2D.linearVelocityY < fallingThreshold))
        {
            Collider2D[] colliders =
                Physics2D.OverlapCircleAll(
                    groundCheck.position,
                    groundedRadius,
                    groundLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    isJumping = false;
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

    private void SetAnimatorState(AnimatorState state)
    {
        switch (state)
        {
            case AnimatorState.Walking:
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", false);
                break;
            case AnimatorState.Jumping:
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsJumping", true);
                animator.SetBool("IsFalling", false);
                break;
            case AnimatorState.Falling:
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", true);
                break;

            case AnimatorState.Idle:
            default:
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", false);
                break;
        }
    }
}
