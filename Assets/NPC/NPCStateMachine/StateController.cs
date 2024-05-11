using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController
{
    public State CurrentState{get;private set;}
   
    public void InitalizeStateController(State currentState){
        this.CurrentState = currentState;
        CurrentState.Enter();
    }
    public void ChangeState(State newState){
        CurrentState.Exit();
        this.CurrentState = newState;
        CurrentState.Enter();
    }
    public void PhysicsUpdate(){
        CurrentState.PhysicsUpdate();
    }
    public void LogicUpdate(){
        CurrentState.LogicUpdate();
    }
    
    
}
