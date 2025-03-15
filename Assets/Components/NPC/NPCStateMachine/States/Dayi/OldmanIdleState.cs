using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldmanIdleState : OldmanState
{
    public OldmanIdleState(StateController sc, NavMeshAgent agent, OldmanNPC oldmanNpc) : base(sc, agent, oldmanNpc)
    {
    }

    public override void Enter()
    {
        base.Enter();
        agent.isStopped=true;
        oldmanNpc.SetActiveQueMark(true);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState && Time.time>=startTime+Random.Range(5f,15f))
        {
            sc.ChangeState(oldmanNpc.OldmanPatrolState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        oldmanNpc.CheckOnIdle();
    }
    public override void Exit()
    {
        base.Exit();
        oldmanNpc.SetActiveQueMark(false);
        agent.isStopped=false;
    }
}
