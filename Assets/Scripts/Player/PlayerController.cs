using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpForceMultiplier = 0.5f;

    [SerializeField] private float groundDistance;
    public float moveDirection { get; private set; }

    public bool isGrounded { get; private set;}
    public bool canMove { get; private set;}
    public bool canDoubleJump { get; private set;}
    private bool isFacingRight = true;
    

    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public AudioSource audioSource;

    [SerializeField] private Transform playerSprite;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

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
        IsGrounded();
        ProcessInputs();
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
            CheckCanJump();
        }
        else if (Input.GetButtonUp("Jump") && rigid.velocity.y > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * jumpForceMultiplier);
        }
    }
    
    private void CheckInputDirection()
    {
        if (isFacingRight && moveDirection < 0)
            Flip();
        else if (!isFacingRight && moveDirection > 0)
            Flip();
    }

    private void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);
    }

    private void ApplyMovement(float dir)
    {
        rigid.velocity = new Vector2(dir * moveSpeed, rigid.velocity.y);
    }

    private void Jump() {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        audioSource.clip = AudioManager.instance.PlayerJumpAudio;
        audioSource.Play();
    }

    private void CheckCanJump() {
        if (isGrounded) 
        {
            Jump();
            canDoubleJump = true;
        } 
        else 
        {
            if (canDoubleJump) {
                Jump();
                canDoubleJump = false;
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        playerSprite.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

    public float MoveDirection() => moveDirection;

    public float Speed() => moveSpeed;
    public void SetPlayerCanMove(bool _canMove) => this.canMove = _canMove;
}
