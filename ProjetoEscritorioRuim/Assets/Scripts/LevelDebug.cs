using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelDebug : MonoBehaviour
{
    
    void Update()
    {
        if (Keyboard.current.ctrlKey.isPressed && Keyboard.current.shiftKey.isPressed && Keyboard.current.rKey.wasPressedThisFrame)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
