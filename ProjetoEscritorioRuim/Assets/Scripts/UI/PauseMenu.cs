using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PauseMenu : MonoBehaviour
{
    //Pause button is "Esc" or "Start" in gamepad
    InputSystem_Actions inputUI;
    InputAction pauseGame;

    private Timer timerScript;
    
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Selectable button;
    
    #region Pause Variables
    public static bool gameIsPaused = false;
    public bool canPause;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject gameHUD;
    [SerializeField] TMPro.TextMeshProUGUI timer;
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
        timerScript = GetComponent<Timer>();
        
        pauseGame = inputUI.UI.Pause;
        pauseGame.Enable();
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        canPause = true;
        gameIsPaused = false;
    }

    void Update()
    {
        if(GameManager.Instance.inConversation || GameManager.Instance.levelCleared)
            canPause = false;
        else
            canPause = true;
        
        if (pauseGame.WasPressedThisFrame() && canPause)
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
        DisplayTimeOnPause(timerScript.timeRemaning);
    }
    #endregion
    #region Pause Menu Methods
    void Pause()//Pauses the game through the time scale and opens the Menu UI
    {
        if (canPause)
        {
            pauseMenuUI.SetActive(true);
            gameHUD.SetActive(false);
            Time.timeScale = 0f;
            gameIsPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            eventSystem.SetSelectedGameObject(button.gameObject);
        }
    }

    public void Resume()//Continues the level you're on
    {
        pauseMenuUI.SetActive(false);
        gameHUD.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
        if (!ComputerTask.isOnComputer)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void RestartLevel()//Reloads the current scene restarting the level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
        //Probably Main Menu
    }
    private void DisplayTimeOnPause(float displayTime)
    {
        if (GameManager.Instance.tasksActive)
        {
            displayTime += 1f;
        
            float minutes = Mathf.FloorToInt(displayTime / 60);
            float seconds = Mathf.FloorToInt(displayTime % 60);
        
            timer.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
        else
            timer.text = "Time: None";
    }
    #endregion
}