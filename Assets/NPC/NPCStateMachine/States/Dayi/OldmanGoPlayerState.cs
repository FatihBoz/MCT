using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldmanGoPlayerState : OldmanState
{
    public OldmanGoPlayerState(StateController sc, NavMeshAgent agent, OldmanNPC oldmanNpc) : base(sc, agent, oldmanNpc)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("goPlayer");
        oldmanNpc.PlayerSeen=false;
        agent.isStopped=false;
        agent.SetDestination(oldmanNpc.LastPlayerSeenPosition);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState && agent.remainingDistance<0.1f && !oldmanNpc.PlayerSeen)
        {
            sc.ChangeState(oldmanNpc.OldmanIdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
