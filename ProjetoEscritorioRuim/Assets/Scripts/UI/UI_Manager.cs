using System;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [SerializeField] private EventSystem eventSystem;

    [Header("-HUD-")]
    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject taskList;
    [SerializeField] private Image[] hotbarSlots;
    [SerializeField] private TextMeshProUGUI[] taskNameText = new TextMeshProUGUI[length];
    
    /*[Header("-Dialogue Box-")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject dialoguePanel;*/
    
    [Header("-EndScreen-")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private Selectable[] buttonToSelect;
    

    private static int length;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
        // taskNameText = GameObject.FindGameObjectsWithTag("taskName");
    }

    private void Start()
    {
        gameHUD.SetActive(true);
        endScreen.SetActive(false);
        taskList.SetActive(false);
        // dialoguePanel.SetActive(false);
        hotbarSlots[0].gameObject.SetActive(false);
        hotbarSlots[1].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.tasksActive)
            taskList.SetActive(true);
    }

    public void EndLevelScreen()
    {
        Time.timeScale = 0f;
        endScreen.SetActive(true);
        gameHUD.SetActive(false);
        finalTimeText.text = "Clear Time: " + FormatEndTimer(ScoreManager.Instance.time, finalTimeText);
        bestTimeText.text = "Best Time: " + FormatEndTimer(ScoreManager.Instance.bestTime, bestTimeText); 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        eventSystem.SetSelectedGameObject(buttonToSelect[0].gameObject);
    }

    private string FormatEndTimer(float displayTime, TextMeshProUGUI timerText)
    {
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOverScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameHUD.SetActive(false);
        gameOverScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(buttonToSelect[1].gameObject);
    }

    public void CheckTaskInList(int taskNumber)
    {
        taskNameText[taskNumber].text = "Completo";
    }

    public void ChangeItemIndicatorState(int i, Color color)
    {
        hotbarSlots[i].color = color;
    }

    public void ToggleItemIndicator(int i)
    {
        if(hotbarSlots[i].IsActive())
            hotbarSlots[i].gameObject.SetActive(false);
        else
            hotbarSlots[i].gameObject.SetActive(true);
    }

    public void UpdateTaskCounter(TextMeshProUGUI taskCounter, string tasksCompleted, string tasksToComplete)
    {
        taskCounter.text = string.Format("Tasks: {0}/{1}", tasksCompleted, tasksToComplete);
    }

    private void OnDisable()
    {
        DialogueController.OnDialogueEnded -= EndLevelScreen;
    }
}
