using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldmanState : State
{
    protected OldmanNPC oldmanNpc;
    public OldmanState(StateController sc, NPCMover npcMover, OldmanNPC oldmanNpc) : base(sc, npcMover)
    {
        this.oldmanNpc=oldmanNpc;
    }
    public override void LogicUpdate(){

        if (oldmanNpc.PlayerSeen && !oldmanNpc.HasImmunity && sc.CurrentState != oldmanNpc.OldmanGoPlayerState)
        {
            sc.ChangeState(oldmanNpc.OldmanGoPlayerState);
        }

        if(!isExitingState && oldmanNpc.Stolen){
            sc.ChangeState(oldmanNpc.OldmanObjectStolenState);
        }

        if(!isExitingState && oldmanNpc.SkillCasted)
        {
            sc.ChangeState(oldmanNpc.OldmanPatrolState);
        }

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
