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
            TogglePause();
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
        Time.timeScale = 0;  // Oyunu durdur
        CrazySDK.Game.GameplayStop();
        PausePanel.SetActive(true);
        CrazySDK.Banner.RefreshBanners();
        isPaused = true;
    }

    public void Continue()
    {
        Time.timeScale = 1;  // Oyunu devam ettir
        CrazySDK.Game.GameplayStart();
        PausePanel.SetActive(false);
        isPaused = false;
    }

    void TogglePause()
    {
        if (isPaused)
        {
            Continue();
        }
        else
        {
            Pause();
        }

        Debug.Log(isPaused ? "Oyun Durduruldu!" : "Oyun Devam Ediyor!");
    }

    public void MainMenu()
    {
        Continue();
        SceneManager.LoadScene(0);
    }


}
