using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuncuController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;
    private int istenenSerit = 1; // 0:sol 1:orta 2:sağ
    public float seritMesafesi = 4;
    public float jumpForce;
    public float Gravity = -20;
    public float timecount = 60;
    public Animator animator;
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(!OyuncuManager.isGameStarted)
            return;

        if (forwardSpeed < maxSpeed && PlayerPrefs.GetInt("pause")==0)
        {
             forwardSpeed += 0.9f * Time.fixedDeltaTime;
        }
        direction.z = forwardSpeed;
        if(SwipeManager.swipeRight)
        {
            istenenSerit++;
            if(istenenSerit == 3)   
                istenenSerit = 2;
            FindObjectOfType<SesYoneticisi>().PlaySound("Slide");
        }

        if(SwipeManager.swipeLeft)
        {
            istenenSerit--;
            if(istenenSerit == -1)   
                istenenSerit = 0;
            FindObjectOfType<SesYoneticisi>().PlaySound("Slide");
        }
    
       
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
   
        if(istenenSerit == 0)
        {
            targetPosition += Vector3.left*seritMesafesi;
        }else if(istenenSerit == 2)
        {
            targetPosition += Vector3.right*seritMesafesi;
        }
        
        if(transform.position == targetPosition)
            return;
            
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 15 * Time.deltaTime;

        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);    

    }

     private void FixedUpdate()
    {
        if(!OyuncuManager.isGameStarted)
            return;

        controller.Move(direction*Time.fixedDeltaTime);
    }

   
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            OyuncuManager.gameOver = true;
            FindObjectOfType<SesYoneticisi>().PlaySound("OyunBitimi");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "slow")
        {
            forwardSpeed -= forwardSpeed * 50 / 100;
            FindObjectOfType<SesYoneticisi>().PlaySound("Kum");
        }

    }

    
}

