using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }


    [Header("-HUD-")]
    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject taskList;
    [SerializeField] private Image[] hotbarSlots;
    [SerializeField] private TextMeshProUGUI[] taskNameText = new TextMeshProUGUI[length];
    
    [Header("-Dialogue Box-")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject dialoguePanel;
    
    [Header("-EndScreen-")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    

    private static int length;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        // taskNameText = GameObject.FindGameObjectsWithTag("taskName");
    }

    private void Start()
    {
        gameHUD.SetActive(true);
        endScreen.SetActive(false);
        taskList.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TaskSystem.Instance.tasksActive)
            taskList.SetActive(true);
    }

    public void EndLevelScreen()
    {
        endScreen.SetActive(true);
        finalTimeText.text = "Clear Time: " + ScoreManager.Instance.time.ToString("F2");
        bestTimeText.text = "Best Time: " + ScoreManager.Instance.bestTime.ToString("F2");
    }

    public void CheckTaskInList(int taskNumber)
    {
        taskNameText[taskNumber].text = taskNameText[taskNumber].text + " - Complete";
    }


    public void ShowDialogue(string[] dialogue, string name, bool activatesTasks)
    {
        if (GameManager.Instance.levelCompletable && activatesTasks)
        {
            dialoguePanel.SetActive(true);
            Time.timeScale = 0f;
            nameText.text = name;
            dialogueText.text = dialogue[1];
            GameManager.Instance.inConversation =  true;
        }
        else
        {
            dialoguePanel.SetActive(true);
            Time.timeScale = 0f;
            nameText.text = name;
            dialogueText.text = dialogue[0];
            GameManager.Instance.inConversation =  true;
        }
    }
    
    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
        dialoguePanel.SetActive(false);
        GameManager.Instance.inConversation = false;
        
        if (GameManager.Instance.levelCleared)
            EndLevelScreen();
        else
            Time.timeScale = 1f;
    }

    public void ChangeItemIndicatorState(int i, Color color)
    {
        hotbarSlots[i].color = color;
    }

    public void UpdateTaskCounter(TextMeshProUGUI taskCounter, string tasksCompleted, string tasksToComplete)
    {
        taskCounter.text = string.Format("Tasks: {0}/{1}", tasksCompleted, tasksToComplete);
    }
}
