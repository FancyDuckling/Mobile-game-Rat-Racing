using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    protected CharacterController controller;
    protected Vector3 direction;
    protected int desiredLane = 1; //0:left, 1:middle, 2:right

    public float forwardSpeed;
    public float maxSpeed;
    public float laneDistance = 4;
    public float gravity = -20;

    
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    
    protected virtual void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        //increase speed 
        if(forwardSpeed < maxSpeed) 
            forwardSpeed += 0.05f * Time.deltaTime;
        
        direction.z = forwardSpeed;

        direction.y += gravity * Time.deltaTime;

       

        //input decides wich lane we should be in
        //TODO switch to touch input
        if (SwipeManager.swipeRight)
        {
            
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
           
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
           
        }

        //calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }

        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

       // transform.position = targetPosition; //Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);
       // controller.center = controller.center; //fixes bug of not colliding with cones


        //this can also fix the bug
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}
