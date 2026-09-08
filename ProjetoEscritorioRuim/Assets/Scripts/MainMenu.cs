using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    #region Declara��o de variaveis
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject taskbar;
    
    [SerializeField] GameObject[] windows;
    
    [SerializeField] GameObject currentWindowParent;
    [SerializeField] GameObject windowParent;
    
    #endregion
    private void Awake()
    {
        titleScreen.SetActive(true);
        mainMenu.SetActive(false);
        taskbar.SetActive(false);
    }

    private void Start()
    {
        
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
        Transform Parent = currentWindowParent.transform;
        print("Opening Window " + i);
        windows[i].SetActive(true);
        windows[i].transform.parent = Parent;
    }
    public void CloseWindow(int i)
    {
        Transform Parent = windowParent.transform;
        print("Closing Window " + i);
        windows[i].SetActive(false);
        windows[i].transform.parent = Parent;
    }
}
