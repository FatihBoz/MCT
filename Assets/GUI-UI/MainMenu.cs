using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Camera mainCamera;
    public bool goingToScene;
    public Transform buttons;
    public Image background;
    private int selectedScene=0;
    private bool loading;

    public TextMeshProUGUI maxScoreText;
    void Start()
    {
        if (!PlayerPrefs.HasKey("maxScore"))
        {
            PlayerPrefs.SetInt("maxScore",0);
        }
        maxScoreText.text="Maximum score is "+ PlayerPrefs.GetInt("maxScore").ToString();
        mainCamera=Camera.main;
        goingToScene=false;
        loading=false;
    }

    void Update()
    {
            if (goingToScene)
            {
                mainCamera.orthographicSize-=Time.deltaTime;
                Color curColor=background.color;
                curColor.a-=Time.deltaTime;
                background.color=curColor;
                if (mainCamera.orthographicSize<=6 && !loading)
                {
                    loading=true;
                SceneManager.LoadScene(selectedScene);
            }

            }
    }
    public void StartButtonPressed(){
        goingToScene=true;
        buttons.gameObject.SetActive(false);
        selectedScene=1;
    }
    public void BetterLevelButtonPressed(){
        goingToScene=true;
        buttons.gameObject.SetActive(false);
            selectedScene=2;
    }
    
}
