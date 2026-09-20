using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameEndScript : MonoBehaviour
{
    public static GameEndScript Instance  { get; private set; }
    
    [SerializeField] EventSystem eventSystem;
    [SerializeField] private Selectable button;
    [SerializeField] private GameObject endScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
        
        // endScreen =  GameObject.FindGameObjectWithTag("EndScreen");
    }
    // Update is called once per frame
    public void EndGame()
    {
        endScreen.SetActive(true);
        Time.timeScale = 0;
        GameManager.Instance.levelCleared =  true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        eventSystem.SetSelectedGameObject(button.gameObject);
    }

    void OnDisable()
    {
        DialogueController.OnDialogueEnded -= EndGame;
    }
}
