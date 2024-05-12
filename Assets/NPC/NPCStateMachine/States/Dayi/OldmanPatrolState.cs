using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldmanPatrolState : OldmanState
{
    public OldmanPatrolState(StateController sc, NavMeshAgent agent, OldmanNPC oldmanNpc) : base(sc, agent, oldmanNpc)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Patrol");
        oldmanNpc.SelectRandomPosition();
        agent.isStopped=false;
        agent.SetDestination(oldmanNpc.CurrentTargetPosition);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState && !agent.pathPending && agent.remainingDistance < 2)
        {   
            sc.ChangeState(oldmanNpc.OldmanIdleState);
        }
    }

}
