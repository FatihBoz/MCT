using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldmanState : State
{
    protected OldmanNPC oldmanNpc;
    public OldmanState(StateController sc, NavMeshAgent agent,OldmanNPC oldmanNpc) : base(sc, agent)
    {
        this.oldmanNpc=oldmanNpc;
    }
    public override void LogicUpdate(){
         if (oldmanNpc.PlayerSeen)
        {
            sc.ChangeState(oldmanNpc.OldmanGoPlayerState);
        }else if(!isExitingState && oldmanNpc.Stolen){
            sc.ChangeState(oldmanNpc.OldmanObjectStolenState);
        }
    }
}
