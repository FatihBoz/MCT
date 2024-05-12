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
        oldmanNpc.SetFalseStolen();
        base.Enter();
        Debug.Log("Object Stolen");
        agent.isStopped=false;
        agent.SetDestination(oldmanNpc.CurrentTargetPosition);
        
        oldmanNpc.SetActiveExcMark(true);
    }
     public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState && !agent.pathPending && agent.remainingDistance < 2)
        {   
            sc.ChangeState(oldmanNpc.OldmanIdleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        oldmanNpc.SetActiveExcMark(false);
    }

}
