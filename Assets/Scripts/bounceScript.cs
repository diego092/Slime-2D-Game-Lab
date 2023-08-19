using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceScript : MonoBehaviour
{
   
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
        }
        else
        {
            Debug.Log("no es player");
        }
    }
}
