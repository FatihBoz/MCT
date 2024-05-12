using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    private float startTime;
    private bool enable;
    private void OnEnable() {
        OldmanNPC.OnLose+=Lost;
    }
    private void Start() {
        enable=false;
    }
    private void OnDisable() {
        
        OldmanNPC.OnLose-=Lost;
    }
   private void Update() {
    if (Time.time>=startTime+5f && enable)
    {
        SceneManager.LoadScene(0);
    }
   }
   public void Lost(){
    if (!enable)
    {
        startTime=Time.time;
    enable=true;
gameOverPanel.SetActive(true);
    }

   }
}
