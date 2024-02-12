using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CarController
{
    public float jumpForce;
    public Animator animator;
    private bool isJumping = false;

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

        // Check if not already jumping
        if (!isJumping)
        {
            // Set jumping flag
            isJumping = true;
            // Set jump trigger
            animator.SetBool("isJumping", true);
            direction.y = jumpForce;
            FindAnyObjectByType<AudioManager>().PlaySound("Jump");

            // Reset the jumping flag after a short delay
            StartCoroutine(ResetJumpFlag());
        }


        /* animator.SetTrigger("jumpingTrigger");
        direction.y = jumpForce;
        FindAnyObjectByType<AudioManager>().PlaySound("Jump");*/

       


        /*if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("jumpingTrigger"); // "JumpTrigger" is the name of the jump animation trigger parameter
        }*/

    }

    private IEnumerator ResetJumpFlag()
    {
        // Wait for a short delay before resetting the flag
        yield return new WaitForSeconds(0.1f);
        // Reset the jump flag
        isJumping = false;
        // Reset the jump trigger in the animator
        animator.SetBool("isJumping", false);
    }

}
