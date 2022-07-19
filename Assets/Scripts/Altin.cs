using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altin : MonoBehaviour
{
    public GameObject alert;
     void Start()
    {
       
    }

    
    void Update()
    {
        transform.Rotate(0,  50 * Time.deltaTime,0);
      

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<SesYoneticisi>().PlaySound("AltınSesi");

            OyuncuManager.altinNumarasi += 10;
            alert.SetActive(true);
            Destroy(gameObject);
        

        }
    }
}
