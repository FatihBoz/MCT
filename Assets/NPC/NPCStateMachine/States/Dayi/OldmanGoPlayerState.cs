using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldmanGoPlayerState : OldmanState
{

    private float pathCreateDuration = 0.4f;
    private float pathTime;
    public OldmanGoPlayerState(StateController sc, NPCMover npcMover, OldmanNPC oldmanNpc) : base(sc, npcMover, oldmanNpc)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(oldmanNpc.gameObject.name + "goPlayer");
        npcMover.isStopped=true;
        
        //oldmanNpc.PlayerSeen=false;
        npcMover.isStopped=false;
        npcMover.CreatePath(oldmanNpc.CurrentTargetPosition);
        oldmanNpc.SetActiveExcMark(true);
        npcMover.speed = oldmanNpc.increasedSpeed;
        pathTime= Time.time;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState && npcMover.reachedEndOfPath && !oldmanNpc.PlayerSeen)
        {
            sc.ChangeState(oldmanNpc.OldmanIdleState);
        }

        if (!isExitingState && Vector2.Distance(oldmanNpc.transform.position, oldmanNpc.CurrentTargetPosition) <= 1.5f && oldmanNpc.PlayerSeen)
        {
            OldmanNPC.OnLose?.Invoke();
        }

        if (Time.time >= pathTime + pathCreateDuration)
        {
            npcMover.CreatePath(oldmanNpc.CurrentTargetPosition);
            pathTime = Time.time;
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
        npcMover.SetIsMoving(false);
        oldmanNpc.SetActiveExcMark(false);
        npcMover.speed = oldmanNpc.baseSpeed;
    }

}
