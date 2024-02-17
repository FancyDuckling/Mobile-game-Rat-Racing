using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    const string IDLEANIM = "IdleAnim";
    const string TILTLEFTANIM = "TiltLeftAnim";
    const string TILTRIGHTANIM = "TiltRightAnim";
    const string JUMPANIM = "JumpAnim";

    private void Start()
    {
        animator = gameObject.GetComponent<Animator> ();
    }

    private void Update()
    {
        if (SwipeManager.swipeLeft)
            ChangeAnimationState(TILTLEFTANIM);

        if (SwipeManager.swipeRight)
            ChangeAnimationState(TILTRIGHTANIM);

        if (SwipeManager.swipeUp)
            ChangeAnimationState(JUMPANIM);

    }

    private void ChangeAnimationState(string newState)
    {

        if (newState == currentState)
            return;

        animator.Play(newState);
        currentState = newState;
    }



   
}
