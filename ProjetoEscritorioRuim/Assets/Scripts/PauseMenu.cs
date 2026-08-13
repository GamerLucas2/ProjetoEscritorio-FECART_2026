using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class PauseMenu : MonoBehaviour
{
    //Pause button is "Esc" or "Start" in gamepad
    InputSystem_Actions inputUI;
    InputAction pauseGame;
    #region Pause Variables
    public static bool gameIsPaused = false;
    [SerializeField]GameObject pauseMenuUI;
    #endregion
    #region Event Functions
    private void OnEnable()
    {
        pauseGame.Enable();
    }

    private void OnDisable()
    {
        pauseGame.Disable();
    }
    private void Awake()
    {
        pauseMenuUI.SetActive(false);
        inputUI = new InputSystem_Actions();
        pauseGame = inputUI.UI.Pause;
        pauseGame.Enable();
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        if (pauseGame.WasPressedThisFrame())
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }
    #endregion
    #region Pause Menu Methods
    void Pause()//Pauses the game through the time scale and opens the Menu UI
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()//Continues the level you're on
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void RestartLevel()//Reloads the current scene restarting the level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitLevel()
    {
        SceneManager.LoadScene("PLACEHOLDER");
        //Probably Main Menu
    }
    #endregion
}