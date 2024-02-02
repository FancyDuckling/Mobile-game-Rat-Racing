using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CarController
{
    public float jumpForce;

    protected override void Update()
    {
        base.Update(); // Call the base class implementation first
        
        if (controller.isGrounded && SwipeManager.swipeUp)
        {
            Jump();
        }
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

}
