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

    public TextMeshProUGUI text;
    private bool stealable;

    public delegate void ObjectStealingHandler(StealableObject obj);
    public static event ObjectStealingHandler OnObjectStolen;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        AnimationsSet();

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
            text.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                OnObjectStolen?.Invoke(collision.gameObject.GetComponent<StealableObject>());
                Destroy(collision.gameObject);
                text.gameObject.SetActive(false);

            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Stealable"))
        {
            text.gameObject.SetActive(false);

        }

    }
}