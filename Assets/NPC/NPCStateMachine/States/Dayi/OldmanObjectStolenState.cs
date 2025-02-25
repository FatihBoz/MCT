using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldmanObjectStolenState : OldmanState
{
    public OldmanObjectStolenState(StateController sc, NPCMover npcMover, OldmanNPC oldmanNpc) : base(sc, npcMover, oldmanNpc)
    {
    }
    public override void Enter()
    {
        oldmanNpc.SetFalseStolen();
        base.Enter();
        Debug.Log(oldmanNpc.gameObject.name + "Object Stolen State");
        npcMover.isStopped = false;
        npcMover.reachedEndOfPath=false;
        npcMover.path = null;
        npcMover.CreatePath(oldmanNpc.CurrentTargetPosition);

        oldmanNpc.SetActiveExcMark(true);
    }
     public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState && !npcMover.pathPending && npcMover.reachedEndOfPath)
        {
            sc.ChangeState(oldmanNpc.OldmanIdleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        oldmanNpc.SetActiveExcMark(false);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        oldmanNpc.CheckOnWalk();
    }

}
