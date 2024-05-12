using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public static Action OnSkillCasted;
    public static PlayerSkill instance;
    
    public bool skillCanBeCasted = true;
    public float cooldown = 30f;

    private Rigidbody2D rb;
    private Animator animator;
    private PlayerMovement movement;
    

    private void Awake()
    {
        instance = this;
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && movement.movement == Vector2.zero && skillCanBeCasted)
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
