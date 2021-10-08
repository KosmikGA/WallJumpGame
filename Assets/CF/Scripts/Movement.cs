using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Movement : MonoBehaviour
{
    public GameManager manager;
    public Animator Animator;

    public float speed;
    public float jumpForce;
    private float moveInput;

    //sprite direction and ground check
    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    private bool isGrounded2;
    public Transform groundCheck2;
    public float checkRadius;
    public LayerMask whatIsGround;

    //wall sliding
    bool isTouchingFront;
    public Transform frontCheck;
    private bool wallSliding;
    public float wallSlidingSpeed;

    //wall jumping
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    //charge jump
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    //double jump
    private int extraJumps;
    public int extraJumpsValue;

    private Rigidbody2D rb;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //horizontal movement
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        Animator.SetFloat("speed", Mathf.Abs(rb.velocity.y));


        //jump if on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        //ground checks
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isGrounded2 = Physics2D.OverlapCircle(groundCheck2.position, checkRadius, whatIsGround);

        //wall touching check and animations
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

        //wall sliding speed
        if (wallSliding == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        //start wall jumping
        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            wallJumping = true;
            Invoke(nameof(SetWallJumping), wallJumpTime);
        }

        if (wallJumping == true)
        {
            isGrounded = true;
            rb.velocity = new Vector2(xWallForce * moveInput, yWallForce);
        }

        if (isGrounded2 == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        //charge jump
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

        //double jump
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }


    }

    //Update is called once per frame
    void FixedUpdate()
    {
        //sprite direction
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
        //gizmos for ground and wall checks
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            manager.GameFail();

        }
    }
}
