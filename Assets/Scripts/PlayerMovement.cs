using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float playerSpeed;
    public float jumpingPower;
    private bool isFacingRight = true;

    [Header("Debug")]
    public bool showGroundCheck;

    // Data inputs.
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float horizontal;
    private float movementDetect;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        GetInput();
        Flip();

        // Debug - Ground check ray.
        if (showGroundCheck)
            Debug.DrawRay(groundCheck.position, new Vector2(0, -0.2f), Color.red);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        // Get player input.
        horizontal = Input.GetAxisRaw("Horizontal");

        // Jup if player input is jump key.
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private void MovePlayer()
    {
        rb.linearVelocity = new Vector2(horizontal * playerSpeed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);
    }
}
