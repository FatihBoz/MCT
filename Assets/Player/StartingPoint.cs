using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartingPoint : MonoBehaviour
{
    public GameObject winPanel;
    public TextMeshProUGUI scoreText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && CheckList.instance.canFinish)
        {
            Debug.Log("canfinish:"+CheckList.instance.canFinish);
            collision.gameObject.SetActive(false);

            winPanel.SetActive(true);

            scoreText.text = "Score : " + collision.gameObject.GetComponent<PlayerMovement>().playerScore;
        }
    }
}
