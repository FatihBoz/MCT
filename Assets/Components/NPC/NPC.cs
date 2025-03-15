using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    protected Animator animator;
    protected NavMeshAgent agent;
    protected StateController sc;
    public void Start()
    {
        sc=new StateController();
            agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    public void Update()
    {
        SetAnimations();

    }
        void SetAnimations()
    {
        animator.SetFloat("velocity_x", agent.velocity.x);
        animator.SetFloat("velocity_y", agent.velocity.y);
        animator.SetBool("isMoving", agent.velocity!=Vector3.zero);
    }
    public void Effected(){
        
    }
}
