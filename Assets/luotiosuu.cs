using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luotiosuu : MonoBehaviour
{
    public GameObject luoti;

    private void OnTriggerEnter(Collider other)
    {
       // bool juttu = other.gameObject.CompareTag("viholli");
       // if (juttu == true)
        //{   
        
            
            Destroy(luoti, 5);
        //}
        
            
    }
    
}
