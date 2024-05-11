using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldmanObjectStolenState : OldmanState
{
    public OldmanObjectStolenState(StateController sc, NavMeshAgent agent, OldmanNPC oldmanNpc) : base(sc, agent, oldmanNpc)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("object stolen "+oldmanNpc.StolenPosition);
        agent.isStopped=true;
        agent.isStopped=false;
        agent.SetDestination(oldmanNpc.transform.position);
    }
     public override void LogicUpdate()
    {
        base.LogicUpdate();
       
        if (!isExitingState && agent.remainingDistance<0.1f)
        {   
            oldmanNpc.SetFalseStolen();
            sc.ChangeState(oldmanNpc.OldmanIdleState);
        }
    }

}
