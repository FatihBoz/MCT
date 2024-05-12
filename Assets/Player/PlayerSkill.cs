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
    

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Debug.Log(skillCanBeCasted);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && rb.velocity == Vector2.zero && skillCanBeCasted)
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
