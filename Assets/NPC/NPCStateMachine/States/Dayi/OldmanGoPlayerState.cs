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
        Debug.Log(oldmanNpc.gameObject.name + "goPlayer");
        agent.isStopped=true;
        if (Vector2.Distance(oldmanNpc.transform.position,oldmanNpc.CurrentTargetPosition)<=1.5f)
        {
            OldmanNPC.OnLose?.Invoke();
        }
        oldmanNpc.PlayerSeen=false;
        agent.isStopped=false;
        agent.SetDestination(oldmanNpc.CurrentTargetPosition);
        oldmanNpc.SetActiveExcMark(true);
        agent.speed = oldmanNpc.increasedSpeed;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState && !agent.pathPending && agent.remainingDistance < 2 && !oldmanNpc.PlayerSeen)
        {
            sc.ChangeState(oldmanNpc.OldmanIdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        oldmanNpc.CheckOnWalk();
    }
    public override void Exit()
    {
        base.Exit();
        oldmanNpc.SetActiveExcMark(false);

    }

}
