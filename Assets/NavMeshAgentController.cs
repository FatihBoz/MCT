using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    [SerializeField] Transform target;

    private Animator animator;
    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        agent.SetDestination(target.position);

        SetAnimations();
    }

    void SetAnimations()
    {
        animator.SetFloat("velocity_x", agent.velocity.x);
        animator.SetFloat("velocity_y", agent.velocity.y);
        animator.SetBool("isMoving",agent.velocity != Vector3.zero);
    }
}
