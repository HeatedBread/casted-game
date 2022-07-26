using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float MoveSpeed;

    private float jumpForce;
    private float jumpForceMultiplier = 0.5f;
    private float doubleJumpsRemaining;
    private float maxJumpCounter;

    private float moveDirection;
    [SerializeField] private float groundDistance;

    private bool isGrounded;

    private bool isFacingRight;
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool canJump;

    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public AudioSource audioSource;

    [SerializeField] private Transform PlayerSprite;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundMask;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        canMove = true;
    }

    private void Update()
    {

        if (!canMove)
        {
            moveDirection = 0;
            return;
        }

        CheckInputDirection();
        CheckGrounded();
        ProcessInputs();
        CheckIfCanJump();
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        ApplyMovement(moveDirection);
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")){
            Jump();
        }
        else if (Input.GetButtonUp("Jump") && rigid.velocity.y > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * jumpForceMultiplier);
        }
    }
    
    private void CheckInputDirection()
    {
        if (isFacingRight && moveDirection > 0)
            Flip();
        else if (!isFacingRight && moveDirection < 0)
            Flip();
    }

    private void CheckIfCanJump()
    {
        if (doubleJumpsRemaining > 0)
            canJump = true;
        else
            canJump = false;

        if (doubleJumpsRemaining < 0)
            doubleJumpsRemaining = 0;
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundDistance, GroundMask);

        if (isGrounded)
            doubleJumpsRemaining = maxJumpCounter;
    }

    private void ApplyMovement(float dir)
    {
        rigid.velocity = new Vector2(dir * MoveSpeed, rigid.velocity.y);
    }

    private void Jump()
    {
        if (isGrounded || canJump)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            audioSource.clip = AudioManager.instance.PlayerJumpAudio;
            audioSource.Play();
        }

        if (!isGrounded && canJump)
        {
            doubleJumpsRemaining -= 1;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        PlayerSprite.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, groundDistance);
    }

    public float MoveDirection() => moveDirection;

    public float Speed() => MoveSpeed;

    public bool IsGrounded() => isGrounded;
}
