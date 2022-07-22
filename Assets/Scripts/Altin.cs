using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altin : MonoBehaviour
{
     void Start()
    {
       
    }

    
    void Update()
    {
     //   transform.Rotate(0,  50 * Time.deltaTime,0);
      

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
          

            OyuncuManager.altinNumarasi += 10;      
            Destroy(gameObject);
          

        }
    }
}
