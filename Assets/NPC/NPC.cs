using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Animator animator;
    protected NPCMover npcMover;
    protected StateController sc;
    public void Start()
    {
        sc=new StateController();
        npcMover = GetComponent<NPCMover>();
        if (animator==null)
        {
        animator = GetComponent<Animator>();
        }

        // agent.updateRotation = false;
        // agent.updateUpAxis = false;
    }

    // Update is called once per frame
    public void Update()
    {
        SetAnimations();

    }
        void SetAnimations()
    {
        animator.SetFloat("velocity_x", npcMover.velocity.x);
        animator.SetFloat("velocity_y", npcMover.velocity.y);
        animator.SetBool("isMoving", npcMover.IsMoving);
    }
    public void Effected(){
        
    }
}
