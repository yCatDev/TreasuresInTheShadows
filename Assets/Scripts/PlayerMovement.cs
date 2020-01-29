using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput*speed, rb.velocity.y);

        if (!facingRight && moveInput>0)
            Flip();
        else if (facingRight && moveInput<0)
            Flip();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            rb.velocity = Vector2.up * jumpForce;
    }

    void Flip()
    {
        facingRight = !facingRight;
        GetComponent<SpriteRenderer>().flipX = facingRight;
    }
}
