using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float skillRadius;
    public LayerMask enemyLayer;

    private Animator animator;

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
        Collider2D[] colliderHits = Physics2D.OverlapCircleAll(transform.position, skillRadius, enemyLayer);
        if (colliderHits.Length == 0)
        {
            return;
        }

        animator.SetTrigger("castSpell");

        foreach (Collider2D hit in colliderHits)
        {
            //Change the event
        }
    }

}
