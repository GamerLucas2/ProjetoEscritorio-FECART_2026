using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    public float time;
    public float bestTime = 99999;
    private string levelName;
    
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
        levelName = "Time-" + SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        bestTime = PlayerPrefs.GetFloat(levelName, 100000);
    }
    
    public void SaveLevelTime(float endTime)
    {
        time = endTime;
        if (time < bestTime)
            bestTime = time;
        
        PlayerPrefs.SetFloat(levelName, bestTime);
        print("Saved Time");
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat(levelName, 100000);
        print(levelName);
    }
}
