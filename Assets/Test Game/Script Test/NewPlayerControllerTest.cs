using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerControllerTest : MonoBehaviour
{
    //Probando si funciona
    //References
    private Rigidbody2D rb_Player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    //Input
    private float xInput;
    //CheckStatus
    [Header("Collision check")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;
    //Animations
    private Animator anim;
    private bool facingRight = true;
    

    // Start is called before the first frame update
    void Start()
    {
        rb_Player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        xInput = Input.GetAxis("Horizontal");
        //CheckGround for jump
        CollisionChecks();
        Movement();
        FlipController();
        AnimationControllers();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }


    }

    private void AnimationControllers()
    {   
         
        bool isMoving = rb_Player.velocity.x != 0;
        anim.SetBool("Run", isMoving);
        anim.SetBool("isGrounded",isGrounded);

        //Jump
        anim.SetFloat("yVelocity",rb_Player.velocity.y);

    }

    private void FlipController() 
    {
        if (rb_Player.velocity.x < 0 && facingRight) 
        {
            Flip();
        }
        else if (rb_Player.velocity.x > 0 && !facingRight) 
        {
            Flip();
        }
    }
    private void Flip() 
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Movement()
    {
        rb_Player.velocity = new Vector2(moveSpeed * xInput, rb_Player.velocity.y);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb_Player.velocity = new Vector2(rb_Player.velocity.x, jumpForce);
        }

    }
    //Collision Check
    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
