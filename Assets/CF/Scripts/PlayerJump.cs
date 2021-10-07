using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D collider;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGroundedRight;
    public Transform feetPosRight;
    private bool isGroundedDown;
    public Transform feetPosDown;
    private bool isGroundedLeft;
    public Transform feetPosLeft;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        isGroundedRight = Physics2D.OverlapCircle(feetPosRight.position, checkRadius, whatIsGround);

        if (isGroundedRight == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.gravityScale = 2;
            rb.velocity = Vector2.up * jumpForce;
        }

        isGroundedLeft = Physics2D.OverlapCircle(feetPosLeft.position, checkRadius, whatIsGround);

        if (isGroundedLeft == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.gravityScale = 2;
            rb.velocity = Vector2.up * jumpForce;
        }

        isGroundedDown = Physics2D.OverlapCircle(feetPosDown.position, checkRadius, whatIsGround);

        if (isGroundedDown == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.gravityScale = 2;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
                rb.gravityScale = 2;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }
        }    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rb.gravityScale = 0;
        }
    }
}
