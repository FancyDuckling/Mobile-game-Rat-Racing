using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane = 1; //0:left, 1:middle, 2:right

    public float forwardSpeed;
    public float laneDistance = 4;
    public float jumpForce;
    public float gravity = -20;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        direction.z = forwardSpeed;
        direction.y += gravity * Time.deltaTime;
        //jump
        if (controller.isGrounded)
        {
            //direction.y = -1;

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                Jump();
            }
        }
       

        //input decides wich lane we should be in
        //TODO switch to touch input
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}
