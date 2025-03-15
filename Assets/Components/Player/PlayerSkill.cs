using System;
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


        if (Input.GetKeyDown(KeyCode.Space)) // sa� a�a�� ve sa� yukar� hareket ederken e'ye bast���n� alg�lam�yor
        {


            if (skillCanBeCasted)
            {
                CastSkill();
                print("SKILL");
            }

        }
    }

    void CastSkill()
    {
        animator.SetTrigger("castSpell");
        OnSkillCasted?.Invoke();
    }
}
