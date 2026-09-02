using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    public float time;
    public float bestTime = 99999;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bestTime = PlayerPrefs.GetFloat("highScore");
    }
    
    public void SaveLevelTime(float endTime)
    {
        time = endTime;
        // string levelName = "Time-" + SceneManager.GetActiveScene().name;
        if (time < bestTime)
            bestTime = time;
        
        PlayerPrefs.SetFloat("highScore", bestTime);
        print("Saved Time");
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("highScore", 100000);
    }
}
