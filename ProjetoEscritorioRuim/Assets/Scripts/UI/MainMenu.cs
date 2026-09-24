
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    #region Declara��o de variaveis
    
    [Header("-Main Menu-")]
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject taskbar;
    
    [Header("-Windows-")]
    [SerializeField] GameObject[] windows;
    [SerializeField] GameObject windowParent;
    private bool isWindowOpen;
    
    [Header("-Times-")]
    [SerializeField] TextMeshProUGUI[] bestTimeText;
    #endregion
    private void Awake()
    {
        titleScreen.SetActive(true);
        mainMenu.SetActive(false);
        taskbar.SetActive(false);
    }

    private void Start()
    {
        TimesText();
    }

    public void PlayButtonPressed()
    {
        titleScreen.SetActive(false);
        mainMenu.SetActive(true);
        taskbar.SetActive(true);
    }

    public void CheckIfWindowOpen(int index)
    {
        if (windows[index].activeSelf)
        {
            CloseWindow(index);
        }
        else if (!windows[index].activeSelf)
        {
            CloseAllWindows();
            OpenWindow(index);
        }
        
    }
    public void LevelSelectionButtonPressed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void OpenWindow(int i)
    {
        print("Opening Window " + i);
        windows[i].SetActive(true);
    }
    public void CloseWindow(int i)
    {
        print("Closing Window " + i);
        windows[i].SetActive(false);
    }

    private void CloseAllWindows()
    {
        foreach (GameObject window in windows)
            window.SetActive(false);
    }

    private void TimesText()
    {
        float level1Time = PlayerPrefs.GetFloat("Time-Level-1");
        float level2Time = PlayerPrefs.GetFloat("Time-Level-2");
        float level3Time = PlayerPrefs.GetFloat("Time-Level-3");
        
        float bestTime = level1Time + level2Time + level3Time;
        
        bestTimeText[0].text = FormatTimer(level1Time, bestTimeText[0]);
        bestTimeText[1].text = FormatTimer(level2Time, bestTimeText[1]);
        bestTimeText[2].text = FormatTimer(level3Time, bestTimeText[2]);

        bestTimeText[3].text = FormatTimer(bestTime, bestTimeText[3]);
    }
    private string FormatTimer(float displayTime, TextMeshProUGUI timerText)
    {
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
