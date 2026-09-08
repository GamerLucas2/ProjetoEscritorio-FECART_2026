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
    
    //! FAZER: Fazer com que o jogo reconheça todos os panels de janela como janelas e coloquem um index neles automaticamente
    
    #endregion
    private void Awake()
    {
        titleScreen.SetActive(true);
        mainMenu.SetActive(false);
        // levelSelection.SetActive(false);
        taskbar.SetActive(false);
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
        print("Opening Window " + i);
        windows[i].SetActive(true);
    }
    public void CloseWindow(int i)
    {
        print("Closing Window " + i);
        windows[i].SetActive(false);
    }
}
