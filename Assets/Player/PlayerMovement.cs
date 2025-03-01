using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using HypeAPI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 movement;
    private Rigidbody2D rb;
    private Animator anim;
    

    public TextMeshProUGUI stealText;
    private bool stealable;

    public TextMeshProUGUI scoreText;
    public int playerScore = 0;

    public float skillRadius;
    public LayerMask enemyLayer;

    public SoundManager sound;

    public static Action OnLose;


    public delegate void ObjectStealingHandler(StealableObject obj);
    public static event ObjectStealingHandler OnObjectStolen;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stealText.gameObject.SetActive(false);
        updateScore();
        GameHooks.TriggerGameStart();

    }
    void Update()
    {
        if (PauseScreen.Instance.IsPaused)
        {
            return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        AnimationsSet();
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
                GameHooks.TriggerStealSomething();
                playerScore += collision.gameObject.GetComponent<StealableObject>().scorePoint;
                sound.PickUp();
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
 

    
}