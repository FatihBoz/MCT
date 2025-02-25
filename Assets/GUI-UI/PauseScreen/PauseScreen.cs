using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{


    public static PauseScreen Instance { get; private set; }
    public bool IsPaused { get; private set; }

    private Button resumeButton;
    private Button retryButton;
    private Button exitButton;

    [SerializeField]
    private GameObject pauseScreen;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        pauseScreen.SetActive(true);
        resumeButton = pauseScreen.transform.Find("RESUME").GetComponent<Button>();
        retryButton = pauseScreen.transform.Find("RETRY").GetComponent<Button>();
        exitButton = pauseScreen.transform.Find("EXIT").GetComponent<Button>();
        resumeButton.onClick.AddListener(Resume);
        retryButton.onClick.AddListener(Retry);
        exitButton.onClick.AddListener(Exit);
        pauseScreen.SetActive(false);

    }
    private void OnEnable()
    {
        resumeButton.onClick.AddListener(Resume);
        retryButton.onClick.AddListener(Retry);
        exitButton.onClick.AddListener(Exit);
    }
    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(Resume);
        retryButton.onClick.RemoveListener(Retry);
        exitButton.onClick.RemoveListener(Exit);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    private void Pause()
    {
        Time.timeScale = 0;
        IsPaused = true;
        pauseScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Resume()
    {
        Time.timeScale = 1;
        IsPaused = false;
        pauseScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Retry()
    {
        Time.timeScale = 1;
        IsPaused = false;
        pauseScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Exit()
    {
        Time.timeScale = 1;
        IsPaused = false;
        pauseScreen.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
