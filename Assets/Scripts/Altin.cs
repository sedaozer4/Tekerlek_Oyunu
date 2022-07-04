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
        transform.Rotate(50 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<SesYoneticisi>().PlaySound("AltınSesi");
            OyuncuManager.altinNumarasi += 1;
            Destroy(gameObject);
        }
    }
}
