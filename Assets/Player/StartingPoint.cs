using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingPoint : MonoBehaviour
{
    public GameObject winPanel;
    public TextMeshProUGUI scoreText;

    private float startTime;
    private bool enable;
    private void Start()
    {
        enable = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && CheckList.instance.canFinish)
        {
            if (!enable)
            {
                startTime = Time.time;
                enable = true;
            }
            Debug.Log("canfinish:"+CheckList.instance.canFinish);
            collision.gameObject.SetActive(false);

            winPanel.SetActive(true);

            scoreText.text = "Score : " + collision.gameObject.GetComponent<PlayerMovement>().playerScore;
            
        }
    }
    private void Update()
    {
        if (Time.time >= startTime + 5f && enable)
        {
            SceneManager.LoadScene(0);
        }
    }
}
