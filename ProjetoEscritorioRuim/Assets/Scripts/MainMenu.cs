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
        PlayerPrefs.SetFloat("Time-Level-1", 99999);
        PlayerPrefs.SetFloat("Time-Level-2", 99999);
        PlayerPrefs.SetFloat("Time-Level-2", 99999);
        
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
        float bestTime = 0;

        for (int i = 0; i < 3; i++)
            bestTime += PlayerPrefs.GetFloat("Time-Level-" + i + 1);
        
        for(int i = 0; i < 3; i++)
            bestTimeText[i].text = PlayerPrefs.GetFloat("Time-Level-" + i + 1).ToString();
    }
}
