using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //References
    public CircleCollider2D collider1; //hitbox de fisicas del jugador en si
    public CircleCollider2D collider2; //hitbox para pegarse a la plataforma el mas grande
    private Collider2D plataforma; //plataforma que se encuentra en el rango de poder pegarse al jugador
    private Collider2D plataformaPegada; //plataforma en la que esta pegado el jugador
    bool pegado_status; //si esta pegado el jugador o no
    private Vector3 relative_position; //posicion relativa entre el jugador y la plataforma a la que esta pegado
    private Vector3 PrevPlatPos; //posicion del frame anterior de la plataforma
    private Vector3 Desplazo; //espacio transportado por la plataforma entre un frame y el siguiente
    
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

       //codigo para pegarse con E

       if(Input.GetKeyDown(KeyCode.E) && !pegado_status && plataforma!=null){
        plataformaPegada=plataforma;
        pegado_status=true;
       
        relative_position= transform.position - plataformaPegada.transform.position;
        transform.position= plataformaPegada.transform.position+ relative_position;
        rb_player.gravityScale =0f;

       }
       else if(Input.GetKeyDown(KeyCode.E) && pegado_status && plataformaPegada!=null){
         plataformaPegada=null;
         pegado_status=false;
         rb_player.gravityScale = 1;

         rb_player.AddForce(Desplazo);

       }
       if(pegado_status== true && plataformaPegada!= null){
       
        transform.position= plataformaPegada.transform.position+ relative_position;
        FindPlatSpeed();
        
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

    //chequear si hay alguna plataforma pegable alrededor del jugador

     private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag=="MovPlat"){
            plataforma=other;
           
        }
        else
        {
            plataforma=null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        plataforma = null;
    }
    //encontrar la velocidad de la plataforma en la que estas conectado
    private void FindPlatSpeed(){
       Desplazo = plataformaPegada.transform.position - PrevPlatPos;
       PrevPlatPos = plataformaPegada.transform.position;
       Desplazo = Desplazo * Time.deltaTime;
      
    }
}
