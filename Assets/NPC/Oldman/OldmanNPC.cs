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
    
    public bool PlayerSeen;
    public Vector3 LastPlayerSeenPosition{get;private set;}
    
    private Vector3 defaultLook;
     private Vector3 look;

     public bool Stolen{get;private set;}
     public Vector3 StolenPosition{get;private set;}
    
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
                LastPlayerSeenPosition=hit.collider.transform.position;
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position,transform.position+look*5);
    
        Gizmos.DrawWireSphere(transform.position,idleCheckDistance);
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
                LastPlayerSeenPosition=player.transform.position;
        }
    }
    public void Oldman_ObjectStolen(StealableObject stealableObject){
        
       StolenPosition=stealableObject.gameObject.transform.position;
        if (Vector3.Distance(StolenPosition,transform.position)<=1000)
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
}
