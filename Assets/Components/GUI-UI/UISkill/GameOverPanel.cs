using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CrazyGames;
public class GameOverPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject PausePanel;
    private bool isPaused = false;


    private float startTime;
    private bool enable;
    private void OnEnable() {
        OldmanNPC.OnLose+=Lost;
    }
    private void Start() {
        enable=false;

        CrazySDK.Init(() => { /** initialization finished callback */ });

    }
    private void OnDisable() {
        
        OldmanNPC.OnLose-=Lost;
    }
   private void Update() {

        if (Time.time>=startTime+5f && enable)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
   public void Lost(){

        GameObject.FindGameObjectWithTag("Player").SetActive(false);

        if (!enable)
        {
            startTime=Time.time;
            enable=true;
            gameOverPanel.SetActive(true);

            CrazySDK.Game.GameplayStop();

        }

    }

    public void Pause()
    {
        CrazySDK.Game.GameplayStop();
        PausePanel.SetActive(true);

        TogglePause();

        CrazySDK.Banner.RefreshBanners();

    }

    public void Continue()
    {
        CrazySDK.Game.GameplayStart();
        PausePanel.SetActive(false);
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        Debug.Log(isPaused ? "Oyun Durduruldu!" : "Oyun Devam Ediyor!");
    }
}
