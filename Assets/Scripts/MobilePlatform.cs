using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{   
    //
    //References
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float speedMovement;

    private int nextPlatform = 1;
    private bool orderPlatforms = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (orderPlatforms && nextPlatform + 1 >= movementPoints.Length) 
        {
            orderPlatforms = false;
        }

        if (!orderPlatforms && nextPlatform  <=0)
        {
            orderPlatforms = true;
        }

        //Para saber si la plataforma esta cerca del siguiente punto,
        //para cambiar la direccion en la que se mueve
        if (Vector2.Distance(transform.position, movementPoints[nextPlatform].position) < 0.1f )
        {
            if (orderPlatforms) 
            {
                nextPlatform += 1;
            }
            else 
            {
                nextPlatform = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, movementPoints[nextPlatform].position, speedMovement * Time.deltaTime);
    }

    /*private void OnCollisionEnter2D(Collision2D  other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            other.transform.SetParent(null);
        }
    }*/
}
