using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelDebug : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.ctrlKey.isPressed && Keyboard.current.shiftKey.isPressed && Keyboard.current.rKey.wasPressedThisFrame)
            RestartLevel();
        
        if (Keyboard.current.ctrlKey.isPressed && Keyboard.current.altKey.isPressed && Keyboard.current.shiftKey.isPressed &&  Keyboard.current.rKey.wasPressedThisFrame)
            RestarGame();
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
