using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelDebug : MonoBehaviour
{
    LevelManager levelManager;

    private void Awake()
    {
        levelManager = gameObject.GetComponent<LevelManager>();
    }

    void Update()
    {
        if (Keyboard.current.ctrlKey.isPressed && Keyboard.current.shiftKey.isPressed && Keyboard.current.rKey.wasPressedThisFrame)
            levelManager.Restart();
        
        if (Keyboard.current.ctrlKey.isPressed && Keyboard.current.altKey.isPressed && Keyboard.current.rKey.wasPressedThisFrame)
            levelManager.BackToMainMenu();
        
        if (Keyboard.current.ctrlKey.isPressed && Keyboard.current.hKey.wasPressedThisFrame)
            ScoreManager.Instance.ResetHighScore();
        
        if (Keyboard.current.ctrlKey.isPressed && Keyboard.current.altKey.isPressed && Keyboard.current.nKey.wasPressedThisFrame)
            levelManager.MoveToNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
        
        if (Keyboard.current.ctrlKey.isPressed && Keyboard.current.altKey.isPressed && Keyboard.current.mKey.wasPressedThisFrame)
            SceneManager.LoadScene("Intermission");
    }
    
    
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    void RestarGame()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;   
        Cursor.visible = true;
    }
}
