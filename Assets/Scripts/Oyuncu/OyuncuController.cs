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

    public Animator animator;

    private bool isSliding = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

   
    void Update()
    {
        if(!OyuncuManager.isGameStarted)
            return;

        if(forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.fixedDeltaTime;
        }
            

        animator.SetBool("isGameStarted",true);    

        direction.z = forwardSpeed;

       
        
       //  if(controller.isGrounded)
      // {
           // direction.y = -1;

            if(SwipeManager.swipeUp)
            {
                 Jump();

            }
     //  }else
       {
            direction.y += Gravity * Time.deltaTime;
       }
       
        if(SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }

        if(SwipeManager.swipeRight)
        {
            istenenSerit++;
            if(istenenSerit == 3)   
                istenenSerit = 2;
        }

        if(SwipeManager.swipeLeft)
        {
            istenenSerit--;
            if(istenenSerit == -1)   
                istenenSerit = 0;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
   
        if(istenenSerit == 0)
        {
            targetPosition += Vector3.left*seritMesafesi;
        }else if(istenenSerit == 2)
        {
            targetPosition += Vector3.right*seritMesafesi;
        }
        
   
        //transform.position = Vector3.Lerp(transform.position, targetPosition, 80*Time.fixedDeltaTime);

        if(transform.position == targetPosition)
            return;
            
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

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

    private  void  Jump()
    {
        direction.y = jumpForce;
         
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            OyuncuManager.gameOver = true;
            FindObjectOfType<SesYoneticisi>().PlaySound("OyunBitimi");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;

        animator.SetBool("isSliding", true);

        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;

        animator.SetBool("isSliding", false);

        isSliding = false;
    }
}

