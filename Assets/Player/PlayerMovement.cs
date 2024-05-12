using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;

    public TextMeshProUGUI stealText;
    private bool stealable;

    public TextMeshProUGUI scoreText;
    private int playerScore = 0;

    public float skillRadius;
    public LayerMask enemyLayer;

    private Animator animator;

    public static Action OnSkillCasted;
    

    public delegate void ObjectStealingHandler(StealableObject obj);
    public static event ObjectStealingHandler OnObjectStolen;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stealText.gameObject.SetActive(false);
        updateScore();

    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        AnimationsSet();

        if (Input.GetKeyDown(KeyCode.E) && movement == Vector2.zero)
        {
           CastSkill();
        }

    }
    private void updateScore()
    {
        scoreText.text = "Score : " + playerScore;
        if (PlayerPrefs.GetInt("maxScore")<playerScore)
        {
            PlayerPrefs.SetInt("maxScore",playerScore);
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }
    private void AnimationsSet()
    {
        anim.SetFloat("velocity_x", movement.x);
        anim.SetFloat("velocity_y", movement.y);
        anim.SetBool("isMoving", movement != Vector2.zero);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Stealable"))
        {
            stealText.gameObject.SetActive(true);
            StealableObject obj = collision.gameObject.GetComponent<StealableObject>();
            stealText.text = "Press 'F' to steal this item : " + obj.name;
            if (Input.GetKey(KeyCode.F))
            {
                OnObjectStolen?.Invoke(obj);
                playerScore += collision.gameObject.GetComponent<StealableObject>().scorePoint;
                Destroy(collision.gameObject);
                updateScore();
                stealText.gameObject.SetActive(false);

            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Stealable"))
        {
            stealText.gameObject.SetActive(false);

        }

    }
    void CastSkill()
    {
        anim.SetTrigger("castSpell");
        OnSkillCasted?.Invoke();  
    }

    
}