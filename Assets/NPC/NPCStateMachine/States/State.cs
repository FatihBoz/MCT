using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    protected NPC npc;
    protected StateController sc;
    
    protected NPCMover npcMover;
    protected float startTime;
    protected bool isExitingState;
    public State(StateController sc, NPCMover npcMover)
    {
        
        this.sc = sc;
        this.npcMover = npcMover;
    }
    public virtual void Enter(){
        isExitingState=false;
        startTime=Time.time;
    }
    public virtual void Exit(){
        isExitingState = true;
    }
    public virtual void LogicUpdate(){

    }
    public virtual void PhysicsUpdate(){

    }



}
