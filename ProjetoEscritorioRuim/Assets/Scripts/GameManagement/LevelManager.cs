using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private bool isIntermission;

    private void Start()
    {
        if(isIntermission)
            levelNumber = PlayerPrefs.GetInt("LevelID");
        else
        {
            levelNumber = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("LevelID", levelNumber);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void MoveToNextLevel(int sceneToLoad)
    {
        if (SceneManager.GetActiveScene().name != "Systems")
            SceneManager.LoadScene(sceneToLoad);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;   
        Cursor.visible = true;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
