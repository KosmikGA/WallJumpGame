using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    private bool isGrounded2;
    public Transform groundCheck2;
    public float checkRadius;
    public LayerMask whatIsGround;

    bool isTouchingFront;
    public Transform frontCheck;
    public bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    private float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    private Rigidbody2D rb;
    private Collider2D collider;
    public Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        Animator.SetFloat("speed", Mathf.Abs(rb.velocity.y));


        //jump if on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isGrounded2 = Physics2D.OverlapCircle(groundCheck2.position, checkRadius, whatIsGround);

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isTouchingFront == true && isGrounded == false && moveInput != 0)
        {
            wallSliding = true;
            Animator.SetBool("isSliding", true);
        }
        else if (isTouchingFront == false)
        {
            wallSliding = false;
            Animator.SetBool("isSliding", false);
        }

        if (wallSliding == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            wallJumping = true;
            Invoke(nameof(SetWallJumping), wallJumpTime);
        }

        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }

        if (isGrounded2 == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
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
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    //Update is called once per frame
    void FixedUpdate()
    {

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void SetWallJumping()
    {
        wallJumping = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.2f));

        if (facingRight == true)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y), new Vector2(0.2f, 0.9f));
        }
        else if (facingRight == false)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(0.2f, 0.9f));
        }
    }

        void Flip()
    {
        facingRight = !facingRight;
        Vector3 Rotate = transform.localScale;
        Rotate.x *= -1;
        transform.localScale = Rotate;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Right")
    //    {
    //        //Flip();
    //        Vector3 Rotate = transform.localScale;
    //        Rotate.x *= -1;
    //        transform.localScale = Rotate;
    //    }
    //    else if (collision.gameObject.tag == "Left")
    //    {
    //        //Flip();
    //        Vector3 Rotate = transform.localScale;
    //        Rotate.x *= 1;
    //        transform.localScale = Rotate;
    //    }
    //}
}
