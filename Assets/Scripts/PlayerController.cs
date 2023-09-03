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
    private int coyote=20;
    public AudioClip JumpSound; //sonido del salto
    public AudioClip LandSound; //sonido cuando te pegas con el piso
    public AudioClip WalkSound; //sonido de caminar
    public AudioSource SoundMaker; //componente que hace el sonido
    private bool walkLoop;
    public Animator anim;
    public SpriteRenderer Sprite;
    private bool FallingComplete=false;
    

    [SerializeField] private Rigidbody2D rb_player;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float jumpForce;
    //Input
    private float xInput;
    private float yInput;

    //Booleans
    //private bool Grounded;
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
       
        
        if (!CheckGround.isGrounded){
            coyote= coyote-1;
            anim.SetBool("OnAir",true);
        }
        else{
           
            coyote=20;
            anim.SetBool("OnAir",false);
      
        }
        
        if(!FallingComplete && anim.GetCurrentAnimatorStateInfo(0).IsName("f_fall_Clip")){
            anim.SetBool("FallCompleted",false);
           
            FallingComplete=true;
            anim.SetBool("FallCompleted",true);
        }
        if(FallingComplete){
            FallingComplete=false;
        }
        
       //xInput = Input.GetAxisRaw("Horizontal");
       //yInput = Input.GetAxisRaw("Vertical");
       walkLoop= !SoundMaker.isPlaying && CheckGround.isGrounded;//hago que walkloop solo sea true cuando no haya un sonido y estes en el piso
        
        if (!pegado_status){
             anim.SetFloat("HorizontalMovement",rb_player.velocity.x);
             anim.SetFloat("VerticalMovement",rb_player.velocity.y);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ){
            rb_player.velocity = new Vector2( -moveSpeed, rb_player.velocity.y );
            Sprite.flipX=true;
            if(walkLoop){
                SoundMaker.PlayOneShot(WalkSound);
            }
        }
        
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ){
            rb_player.velocity = new Vector2( moveSpeed, rb_player.velocity.y );
            Sprite.flipX=false;
            if(walkLoop){
                SoundMaker.PlayOneShot(WalkSound);
            } 
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ){
            rb_player.velocity = new Vector2( 0, rb_player.velocity.y );
        }

       //Debug.DrawRay(transform.position, Vector3.down * 1f, Color.red);

        //Ver si esto es correcto, tanto el sonido como la animación
       if (CheckGround.isGrounded ==true) 
       {

            if (CheckGround.isGrounded ==false && !SoundMaker.isPlaying){
               
                SoundMaker.PlayOneShot(LandSound); //tocar sonido cuando no estes grounded y tocas el piso
            }
            anim.SetBool("JumpStart",false);
            CheckGround.isGrounded = true; 
            
       }
       else 
        {
            CheckGround.isGrounded = false;
        }

        
       if (Input.GetKeyDown(KeyCode.W  ) &&(CheckGround.isGrounded || coyote>0) && !pegado_status || Input.GetKeyDown( KeyCode.UpArrow) && (CheckGround.isGrounded || coyote>0)  && !pegado_status && coyote>0) 
       {
            
            SoundMaker.PlayOneShot(JumpSound); //tocar sonido
            anim.SetBool("JumpStart",true);
            
            Jump();
            Debug.Log("animacion terminada");
            
            
            
            
            
       }

       //codigo para pegarse con Space

       if(Input.GetKeyDown(KeyCode.Space) && !pegado_status && plataforma!=null){
        plataformaPegada=plataforma;
        pegado_status=true;
        Debug.Log(plataformaPegada.transform.position);
        relative_position= transform.position - plataformaPegada.transform.position;
        if (relative_position.x>2){
            relative_position.x=2;
        }
        else if (relative_position.x<-2){
            relative_position.x=-2;
        }
        if (relative_position.y>1){
            relative_position.y=1;
        }
        else if(relative_position.y<-1){
            relative_position.y=-1;
        }
        transform.position= plataformaPegada.transform.position+ relative_position;
        rb_player.gravityScale =0f;
        SoundMaker.PlayOneShot(LandSound);
       }
       else if(Input.GetKeyDown(KeyCode.Space) && pegado_status && plataformaPegada!=null){
         plataformaPegada=null;
         pegado_status=false;
         transform.position= transform.position+Desplazo*Time.deltaTime;
         rb_player.AddForce(Desplazo*15);
         rb_player.gravityScale =1;
 
         
        Debug.Log(Desplazo);

       }

        if(pegado_status== true && plataformaPegada!= null){
            
            transform.position= plataformaPegada.transform.position+ relative_position;
           
            FindPlatSpeed();
        
        }

       
       
    }

    private void FixedUpdate()
    {
       
        //rb_player.velocity = new Vector2(xInput * moveSpeed, rb_player.velocity.y );
       
    }

    private void Jump() 
    {
      
            rb_player.AddForce(Vector2.up * jumpForce);
        
        
    }

    //chequear si hay alguna plataforma pegable alrededor del jugador

     private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag=="MovPlat"){
            plataforma=other;
           
        }
        /*else
        {
            plataforma=null;
        }*/
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag=="MovPlat"){
            plataforma=null;
           
        }
       
    }
    //encontrar la velocidad de la plataforma en la que estas conectado
    private void FindPlatSpeed(){
       Desplazo =plataformaPegada.transform.position-PrevPlatPos;
       PrevPlatPos=plataformaPegada.transform.position;
      
       Desplazo= Desplazo/Time.deltaTime;
      
    }
    IEnumerator DelayCambio(float delay=0){
       
        yield return new WaitForSeconds(delay);
        
        anim.SetBool("JumpStart",false);
         
       
        
    }
    
    
   /* private bool Grounded(){
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1);
    }*/
}
