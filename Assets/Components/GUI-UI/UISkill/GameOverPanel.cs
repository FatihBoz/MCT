using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CrazyGames;
public class GameOverPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject PausePanel;


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

        GameObject.FindGameObjectWithTag("Player").SetActive(false);

        if (!enable)
        {
            startTime=Time.time;
            enable=true;
            gameOverPanel.SetActive(true);
        }

   }

    public void Pause()
    {
        CrazySDK.Game.GameplayStop();
        PausePanel.SetActive(true);

    }

    public void Continue()
    {
        CrazySDK.Game.GameplayStart();
        PausePanel.SetActive(false);
    }


}
