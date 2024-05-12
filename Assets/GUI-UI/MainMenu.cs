using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        mainCamera=Camera.main;
        goingToScene=false;
        loading=false;
    }

    void Update()
    {
            if (goingToScene)
            {
                mainCamera.orthographicSize-=Time.deltaTime*5;
                Color curColor=background.color;
                curColor.a-=Time.deltaTime*5;
                background.color=curColor;
                if (mainCamera.orthographicSize<=6 && !loading)
                {
                    loading=true;
                    StartCoroutine(LoadYourAsyncScene());
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
    
    IEnumerator LoadYourAsyncScene()
    { 
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(selectedScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
