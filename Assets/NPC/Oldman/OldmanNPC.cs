using System;
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

    public float baseSpeed;
    public float increasedSpeed;

    public bool PlayerSeen;
    public bool SkillCasted;
    public bool HasImmunity;
    public Vector3 LastPlayerSeenPosition{get;private set;}

    private Vector3 defaultLook;
     private Vector3 look;

     public bool Stolen{get;private set;}
     public Vector3 StolenPosition{get;private set;}
    [SerializeField]
    private GameObject excMark;
    [SerializeField]

    private GameObject queMark;

    private float patrolTimeAfterSkillCasted = 0f;


    public static Action OnLose;
    private void OnEnable() {
        PlayerMovement.OnObjectStolen+=Oldman_ObjectStolen;
        PlayerSkill.OnSkillCasted += Oldman_OnSkillCasted;
    }

    private void Oldman_OnSkillCasted()
    {
        SkillCasted = true;
        HasImmunity = true;

        npcMover.speed = baseSpeed;
    }

    private void OnDisable() {
        PlayerMovement.OnObjectStolen-=Oldman_ObjectStolen;
        PlayerSkill.OnSkillCasted -= Oldman_OnSkillCasted;
    }
    public new void Start()
    {
        base.Start();
        defaultLook=new Vector3(0,-1,0);
        PlayerSeen=false;
        OldmanIdleState=new OldmanIdleState(sc, npcMover, this);
        OldmanPatrolState=new OldmanPatrolState(sc, npcMover, this);
        OldmanGoPlayerState=new OldmanGoPlayerState(sc, npcMover, this);
        OldmanObjectStolenState=new OldmanObjectStolenState(sc, npcMover, this);

        if (excMark==null)
        {
        excMark =transform.Find("excMark").gameObject;

        }
        SetActiveExcMark(false);

        if (queMark==null)
        {
           
        queMark =transform.Find("queMark").gameObject;
        }
        SetActiveQueMark(false);

        sc.InitalizeStateController(OldmanIdleState);

        npcMover.speed = baseSpeed;
    }
    public new void Update()
    {
        base.Update();
        sc.LogicUpdate();

        look=npcMover.velocity.normalized;
        if (look==Vector3.zero)
        {
            look=defaultLook;
        }

        if (HasImmunity)
        {
            if (patrolTimeAfterSkillCasted >= 5f)
            {
                HasImmunity = false;
                patrolTimeAfterSkillCasted = 0;
                return;
            }

            patrolTimeAfterSkillCasted += Time.deltaTime;
        }
    }
    void FixedUpdate() {
        sc.PhysicsUpdate();
        DetectPlayer();
    }
    public void DetectPlayer(){

         if (!HasImmunity)
        {

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
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position,transform.position+look*5);

        Gizmos.DrawWireSphere(transform.position,idleCheckDistance);
        Gizmos.DrawWireSphere(transform.position,walkingCheckDistance);

        Gizmos.DrawWireSphere(transform.position,stolenDetectDistance);
    }
    public void SelectRandomPosition(){
        int randomValue=UnityEngine.Random.Range(0,positions.childCount);
        CurrentTargetPosition=positions.GetChild(randomValue).position;
    }
    public void CheckOnIdle(){
         if (!HasImmunity)
        {
            Collider2D player=Physics2D.OverlapCircle(transform.position,idleCheckDistance,LayerMask.GetMask("Player"));
            if (player!=null)
            {

                 PlayerSeen=true;
                CurrentTargetPosition=player.transform.position;
            }
        }
    }
    public void CheckOnWalk(){
        if (!HasImmunity)
        {

        Collider2D player=Physics2D.OverlapCircle(transform.position,walkingCheckDistance,LayerMask.GetMask("Player"));

        if (player!=null)
        {
            PlayerSeen=true;
            CurrentTargetPosition=player.transform.position;
        }
        else if(PlayerSeen)
        {
          PlayerSeen = false;
        }
        }
    }
    public void Oldman_ObjectStolen(StealableObject stealableObject){

        CurrentTargetPosition=stealableObject.GetComponent<Collider2D>().bounds.center;
        if (Vector3.Distance(StolenPosition,transform.position)<=stolenDetectDistance && !HasImmunity)
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
    public void SetFalseSkillCasted()
    {
        SkillCasted = false;
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
