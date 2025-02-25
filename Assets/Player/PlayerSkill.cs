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

        if (PauseScreen.Instance.IsPaused)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // sað aþaðý ve sað yukarý hareket ederken e'ye bastýðýný algýlamýyor
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
