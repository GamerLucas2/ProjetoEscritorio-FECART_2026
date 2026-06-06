using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIPlaceHolder : MonoBehaviour
{
    #region Declaração de variaveis
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelection;
    #endregion
    private void Awake()
    {
        titleScreen.SetActive(true);
        mainMenu.SetActive(false);
        levelSelection.SetActive(false);
    }
    public void PlayButtonPressed()
    {
        titleScreen.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void LevelSelect()
    {
        levelSelection.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void LevelButtonPressed(public int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
