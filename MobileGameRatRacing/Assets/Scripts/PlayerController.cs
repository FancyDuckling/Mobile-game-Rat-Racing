using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CarController
{
    public float jumpForce;


    protected override void Update()
    {
        base.Update(); 

        if (controller.isGrounded && SwipeManager.swipeUp)
        { 
            Jump();
        }

    }

    private void Jump()
    {   
         direction.y = jumpForce;
         FindAnyObjectByType<AudioManager>().PlaySound("Jump");
    }

    

}
