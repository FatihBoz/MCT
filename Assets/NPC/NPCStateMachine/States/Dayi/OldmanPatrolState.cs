using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class OldmanPatrolState : OldmanState
{
    public OldmanPatrolState(StateController sc, NPCMover npcMover, OldmanNPC oldmanNpc) : base(sc, npcMover, oldmanNpc)
    {
    }

    public override void Enter()
    {
        base.Enter();
        oldmanNpc.SetFalseSkillCasted();
        Debug.Log(oldmanNpc.gameObject.name + "Patrol");
        oldmanNpc.SelectRandomPosition();
        npcMover.isStopped = false;
        npcMover.reachedEndOfPath=false;
        npcMover.path = null;
        npcMover.CreatePath(oldmanNpc.CurrentTargetPosition);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState && !npcMover.pathPending && npcMover.reachedEndOfPath)
        {
            sc.ChangeState(oldmanNpc.OldmanIdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        oldmanNpc.CheckOnWalk();
    }

}
