using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI timerText;

    [Header("Timer")] 
    [SerializeField] float timeRemaning = 90f;
    [SerializeField] private float elapsedTime = 0f;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameHud;
    PauseMenu pauseMenu;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
    }
    void Update()
    {
        if (TaskSystem.Instance.tasksActive && !GameManager.Instance.levelCleared)
        {
            ContUpTime();
            ContDownTime();
            DisplayTime(timeRemaning);
        }
        
        if (GameManager.Instance.levelCleared)
            SetFinalTime();
    }

    private void DisplayTime(float displayTime)
    {
        displayTime += 1f;
        
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    private void ContDownTime()
    {
        if (timeRemaning > 0)
            timeRemaning -= Time.deltaTime;
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            timeRemaning = 0;
            gameHud.SetActive(false);
            gameOverPanel.SetActive(true);
            PauseMenu.gameIsPaused = true;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void ContUpTime()
    {
        elapsedTime += Time.deltaTime;
    }
    
    private void SetFinalTime()
    {
        float finalTime = elapsedTime;
        print(finalTime);
        
        ScoreManager.Instance.SaveLevelTime(finalTime);
    }
}
