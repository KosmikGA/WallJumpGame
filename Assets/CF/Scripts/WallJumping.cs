using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumping : MonoBehaviour
{
    public float walkSpeed;
    private float moveInput;
    public float jumpSpeed;
    private bool isGrounded;


    public bool isTouchingLeft;
    public bool isTouchingRight;
    public bool wallJumping;
    private float touchingLeftOrRight;

    private float jumpTimeCounter;
    public float jumpTime;


    private Rigidbody2D rb;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if ((!isTouchingLeft && !isTouchingRight) || isGrounded)
        {
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.2f), 0f, whatIsGround);

        isTouchingLeft = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(0.9f, 0.2f), 0f, whatIsGround);

        isTouchingRight = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y), new Vector2(0.9f, 0.2f), 0f, whatIsGround);

        if (isTouchingLeft && Input.GetKeyDown(KeyCode.Space))
        {
            touchingLeftOrRight = 1;
            //Debug.Log("Left");
        }
        else if (isTouchingRight && Input.GetKeyDown(KeyCode.Space))
        {
            touchingLeftOrRight = -1;
            //Debug.Log("Right");
        }

        if (Input.GetKeyDown(KeyCode.Space) && (isTouchingLeft || isTouchingRight) && !isGrounded)
        {
            wallJumping = true;
            jumpTimeCounter = jumpTime;
            rb.gravityScale = 2;
            rb.velocity = Vector2.up * jumpSpeed;
            Invoke(nameof(setJumpingToFalse), 0.08f);
        }

        if (wallJumping)
        {
            rb.velocity = new Vector2(walkSpeed * touchingLeftOrRight, jumpSpeed);
        }

        if (Input.GetKey(KeyCode.Space) && wallJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpSpeed;
                jumpTimeCounter -= Time.deltaTime;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.2f));

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(0.2f, 0.9f));
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y), new Vector2(0.2f, 0.9f));
    }

    void setJumpingToFalse()
    {
        wallJumping = false;
    }
}
