using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceScript : MonoBehaviour
{
    public Animator anim;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player")){
          collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0,150,0));
          anim.SetBool("Salteado",true);
          StartCoroutine(DelayCambio(anim.GetCurrentAnimatorStateInfo(0).length));
        }
        else
        {
            Debug.Log("no es player");
        }
    }
    IEnumerator DelayCambio(float delay=0){
        yield return new WaitForSeconds(delay);
        anim.SetBool("Salteado",false);
    }
}
