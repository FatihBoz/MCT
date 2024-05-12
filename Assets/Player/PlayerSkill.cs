using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float skillRadius;
    public LayerMask enemyLayer;

    private Animator animator;

    public static Action OnSkillCasted;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           CastSkill();
        }
    }

    void CastSkill()
    {
        
        animator.SetTrigger("castSpell");
        OnSkillCasted?.Invoke();
       
    }

}
