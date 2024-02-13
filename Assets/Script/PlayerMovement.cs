using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private float jumpForce = 14f;
    private float moveSpeed = 7f;
    private float dirx = 0f;
    private BoxCollider2D collider;
    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { Jumping, Running, Idle, Falling };
    [SerializeField] private MovementState state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirx = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveSpeed * dirx, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(0, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if(dirx > 0)
        {
            state = MovementState.Running;
            sprite.flipX = false;
        }
        else if(dirx < 0)
        {
            state = MovementState.Running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.Idle;
        }
        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.Jumping;
        }
        if(rb.velocity.y < -0.1f)
        {
            state = MovementState.Falling;
        }
        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
