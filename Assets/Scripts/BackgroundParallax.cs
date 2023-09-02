using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{   
    //References

    [SerializeField] private Vector2 speedMovementB;

    private Vector2 offset;

    private Material material;
   

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        
    }

    private void Update()
    {
        offset =  speedMovementB * Time.deltaTime; 
        material.mainTextureOffset += offset;
    }
}
