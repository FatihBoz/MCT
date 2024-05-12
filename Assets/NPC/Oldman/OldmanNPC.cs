using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldmanNPC : NPC
{
    public Transform positions;
    public Vector3 CurrentTargetPosition{get;private set;}
    public OldmanIdleState OldmanIdleState{get;private set;}
    public OldmanGoPlayerState OldmanGoPlayerState{get;private set;}
    public OldmanPatrolState OldmanPatrolState{get;private set;}
    public OldmanObjectStolenState OldmanObjectStolenState{get;private set;}
    
    public float idleCheckDistance;
    public float walkingCheckDistance;
    public float stolenDetectDistance;
    
    public bool PlayerSeen;
    public Vector3 LastPlayerSeenPosition{get;private set;}
    
    private Vector3 defaultLook;
     private Vector3 look;

     public bool Stolen{get;private set;}
     public Vector3 StolenPosition{get;private set;}
    private GameObject excMark;
    private GameObject queMark;
    private void OnEnable() {
        PlayerMovement.OnObjectStolen+=Oldman_ObjectStolen;
    }
    private void OnDisable() {
         PlayerMovement.OnObjectStolen-=Oldman_ObjectStolen;
    }
    public new void Start()
    {
        base.Start();
        defaultLook=new Vector3(0,-1,0);
        PlayerSeen=false;
        OldmanIdleState=new OldmanIdleState(sc,agent,this);
        OldmanPatrolState=new OldmanPatrolState(sc,agent,this);
        OldmanGoPlayerState=new OldmanGoPlayerState(sc,agent,this);
        OldmanObjectStolenState=new OldmanObjectStolenState(sc,agent,this);


        excMark=transform.Find("excMark").gameObject;
        SetActiveExcMark(false);
        
        queMark=transform.Find("queMark").gameObject;
        SetActiveQueMark(false);

        sc.InitalizeStateController(OldmanIdleState);



    }
    void Update()
    {
        base.Update();
        sc.LogicUpdate();
        look=agent.velocity.normalized;
        if (look==Vector3.zero)
        {
            look=defaultLook;   
        }
    }
    void FixedUpdate() {
        sc.PhysicsUpdate();
        DetectPlayer();
    }
    public void DetectPlayer(){
       
        RaycastHit2D hit = Physics2D.Raycast(transform.position, look);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                PlayerSeen=true;
                CurrentTargetPosition=hit.collider.transform.position;
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position,transform.position+look*5);
    
        Gizmos.DrawWireSphere(transform.position,idleCheckDistance);
        Gizmos.DrawWireSphere(transform.position,walkingCheckDistance);
    }
    public void SelectRandomPosition(){
        int randomValue=Random.Range(0,positions.childCount);
        CurrentTargetPosition=positions.GetChild(randomValue).position;
    }
    public void CheckOnIdle(){
        Collider2D player=Physics2D.OverlapCircle(transform.position,idleCheckDistance,LayerMask.GetMask("Player"));
        if (player!=null)
        {
             PlayerSeen=true;
             SetActiveExcMark(true);
                CurrentTargetPosition=player.transform.position;
        }
    }
    public void CheckOnWalk(){
        Collider2D player=Physics2D.OverlapCircle(transform.position,walkingCheckDistance,LayerMask.GetMask("Player"));
        if (player!=null)
        {
            PlayerSeen=true;
            SetActiveExcMark(true);
            CurrentTargetPosition=player.transform.position;
        }
    }
    public void Oldman_ObjectStolen(StealableObject stealableObject){
        
        CurrentTargetPosition=stealableObject.gameObject.transform.position;
        if (Vector3.Distance(StolenPosition,transform.position)<=stolenDetectDistance)
        {
            Stolen=true;
        }else
        {
            Stolen=false;
        }
    }
    public void SetFalseStolen(){
        Stolen=false;
    }
    public float GetDistance(){
        return Vector3.Distance(transform.position,CurrentTargetPosition);
    }
    public void SetActiveExcMark(bool state){
        excMark.SetActive(state);
    } public void SetActiveQueMark(bool state){
        queMark.SetActive(state);
    }
}
