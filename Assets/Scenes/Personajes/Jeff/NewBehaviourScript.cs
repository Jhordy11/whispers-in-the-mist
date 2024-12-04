using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float runSpeed = 7;
    public float rotationSpeed = 250;
    public Animator animator;
    private float horizontalInput;
    private bool lookRight = true;
    private bool walk = false;
    private float lastHorizontalInput = 0f;
    private float transitionTime = 0.5f;
    void Start()
    {

    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        GestionarOrientacion();

    }

    void GestionarOrientacion()
    {
        if ((lookRight && horizontalInput < 0) || (!lookRight && horizontalInput > 0))
        {
            lookRight = !lookRight;
            animator.SetTrigger("Turn");
        }

        
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(lastHorizontalInput) > 0.1f)
        {
            walk = true;
        }
        else
        {
            walk = false;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if ((stateInfo.IsName("Turn place right") || stateInfo.IsName("Turno place left")||stateInfo.IsName("Turn right") || stateInfo.IsName("Turn left")) && stateInfo.normalizedTime < 1.0f)
        {
            walk = false;
        }

        animator.SetBool("Walk", walk);

        if(walk){
            transform.Translate(0, 0, horizontalInput * Time.deltaTime * runSpeed);
        }
        lastHorizontalInput = Mathf.Lerp(lastHorizontalInput, horizontalInput, transitionTime );
    }

}
