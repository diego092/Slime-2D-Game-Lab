using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    //References
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    //Fisicas
    Rigidbody2D playerRb;
    //Sprite Renderer
    [SerializeField] SpriteRenderer spriteRenderer;
    //Animator
    [SerializeField] Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d") || Input.GetKey("right")) 
        {
            playerRb.velocity = new Vector2(runSpeed, playerRb.velocity.y);
            //Flip imagen
            spriteRenderer.flipX = false;
            //Animaciones
            animator.SetBool("Run", true);
        }

        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            playerRb.velocity = new Vector2(-runSpeed, playerRb.velocity.y);
            //Flip imagen
            spriteRenderer.flipX = true;
            //Animaciones
            animator.SetBool("Run", true);
        }
        else 
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
            animator.SetBool("Run", false);
        }

        if (Input.GetKey("w") && CheckGround.isGrounded || Input.GetKey("up") && CheckGround.isGrounded)
        {
            Jump();
        }
        if (CheckGround.isGrounded == false)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
        if (CheckGround.isGrounded == true) 
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Falling", false);

        }

        if (playerRb.velocity.y < 0) 
        {
            animator.SetBool("Falling", true);
        }
        else if (playerRb.velocity.y > 0) 
        {
            animator.SetBool("Falling", false);
        }
    }

    private void Jump() 
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
    }


}
