using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    // Movement
    public float playerSpeed;
    private bool isFacingRight = true;

    // Jumping
    public float jumpingPower;

    // Dashing
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCoolDown;

    [Header("Debug")]
    public bool showGroundCheck;

    // Data inputs.
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float horizontal;
    private Rigidbody2D rb;
    private TrailRenderer tr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        tr = GetComponent<TrailRenderer>();
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
        // Don't get player input if dashing.
        if (isDashing)
        {
            return;
        }

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

        // Dash if player input is dash key.
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void MovePlayer()
    {
        if (isDashing)
        {
            return;
        }

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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }
}
