using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //References
    [SerializeField] private Rigidbody2D rb_player;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float jumpForce;
    //Input
    private float xInput;
    private float yInput;

    //Booleans
    private bool Grounded;
    private void Awake()
    {
        Rigidbody2D rb_player = GetComponent<Rigidbody2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       xInput = Input.GetAxisRaw("Horizontal");
       yInput = Input.GetAxisRaw("Vertical");

       Debug.DrawRay(transform.position, Vector3.down * 0.8f, Color.red);

       if (Physics2D.Raycast(transform.position, Vector3.down, 0.8f)) 
       {
            Grounded = true; 
       }
       else 
        {
            Grounded= false;
        }


       if (Input.GetKeyDown(KeyCode.Space  ) && Grounded || Input.GetKeyDown( KeyCode.UpArrow) && Grounded) 

       {
            Jump();
       }
    }

    private void FixedUpdate()
    {
        rb_player.velocity = new Vector2(xInput * moveSpeed, rb_player.velocity.y );
    }

    private void Jump() 
    {
        rb_player.AddForce(Vector2.up * jumpForce);
    }

}
