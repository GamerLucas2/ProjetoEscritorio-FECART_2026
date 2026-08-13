using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI timerText;
    
    [Header("Timer")]
    [SerializeField] float timeRemaning = 90f;
    [SerializeField] private float elapsedTime = 0f;
    
    void Update()
    {
        if (TaskSystem.Instance.tasksActive && !GameManager.Instance.levelCleared)
        {
            ContUpTime();
            ContDownTime();
            DisplayTime(timeRemaning);
        }
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
            timeRemaning = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void ContUpTime()
    {
        elapsedTime += Time.deltaTime;
    }
}
