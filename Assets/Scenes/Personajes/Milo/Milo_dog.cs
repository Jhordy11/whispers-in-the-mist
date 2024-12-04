using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo_dog : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -3f); 
    public float followDistance = 3f; 
    public float rotationSpeed = 5f; 
    private Animator animator;
    private Animator playerAnimator; 
    private bool seguir = true;
    void Start()
    {

        animator = GetComponentInChildren<Animator>();

        playerAnimator = target.GetComponent<Animator>();

    }

    void Update()
    {
        if(!seguir){
            return;
        }
        FollowTarget(); 
    }

    public void Seguir(){
        seguir = true;
    }
    public void DejarDeSeguir(){
        seguir = false;
        animator.SetFloat("Speed", 0f);
    }

    void FollowTarget()
    {
        if (target == null || animator == null) return;

        Vector3 targetPosition = target.position + target.TransformDirection(offset);
        targetPosition.y = transform.position.y;

        float distanceToTarget = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                                  new Vector3(targetPosition.x, 0, targetPosition.z));

        
        float playerSpeed = playerAnimator.GetFloat("Speed");
        animator.SetFloat("Speed", playerSpeed);

        if (distanceToTarget <= followDistance)
        {
            animator.SetFloat("Speed", 0f);
            return;
        }

        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, playerSpeed * Time.deltaTime);
    }
}
