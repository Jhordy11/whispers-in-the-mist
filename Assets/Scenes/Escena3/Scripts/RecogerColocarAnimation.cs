using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
public class RecogerColocarAnimation : StateMachineBehaviour
{
    private RockInt rockIntScript;
    private ThirdPersonController thirdPersonController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        rockIntScript = player.GetComponent<RockInt>();
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        if(stateInfo.IsTag("colocar")){
            rockIntScript.PlaceRockInSlot(rockIntScript.GetSlot()); 
        }
        
        thirdPersonController.SetInAnimations(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(stateInfo.IsTag("recoger")){
            rockIntScript.PickupRock(rockIntScript.GetNearbyRock());
        }
        thirdPersonController.SetInAnimations(false);

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
