using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed=5f;
    private Rigidbody2D rb;
    private Animator anim;
    Vector2 movement;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }

    void Update()
    {
        movement.x=Input.GetAxisRaw("Horizontal");
        movement.y=Input.GetAxisRaw("Vertical");
        movement.Normalize();
        AnimationsSet();
    }
    private void FixedUpdate() {
           rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
    }
    private void AnimationsSet(){
        anim.SetFloat("velocity_x",movement.x);
        anim.SetFloat("velocity_y",movement.y);
        anim.SetBool("isMoving",movement!=Vector2.zero);
    }
}
