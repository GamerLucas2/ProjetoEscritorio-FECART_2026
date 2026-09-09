using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
        bestTimeText[0].text = "Level 1 Time: " + PlayerPrefs.GetFloat("Time-Level-1").ToString("F2");
        bestTimeText[1].text = "Level 2 Time: " + PlayerPrefs.GetFloat("Time-Level-2").ToString("F2");
        bestTimeText[2].text = "Level 3 Time: " + PlayerPrefs.GetFloat("Time-Level-3").ToString("F2");

        float bestTime = PlayerPrefs.GetFloat("Time-Level-1") + PlayerPrefs.GetFloat("Time-Level-2") + PlayerPrefs.GetFloat("Time-Level-3");
        
        bestTimeText[3].text = "Total Time: " + bestTime.ToString("F2");
    }
}
