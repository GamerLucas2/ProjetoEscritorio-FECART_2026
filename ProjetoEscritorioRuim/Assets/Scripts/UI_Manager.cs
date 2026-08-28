using System;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject taskList;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject dialoguePanel;
    

    private static int length;
    [SerializeField] private TextMeshProUGUI[] taskNameText = new TextMeshProUGUI[length];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
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
    }

    public void CheckTaskInList(int taskNumber)
    {
        taskNameText[taskNumber].text = taskNameText[taskNumber].text + " - Complete";
    }


    public void ShowDialogue(string[] dialogue, string name)
    {
        dialoguePanel.SetActive(true);
        nameText.text = name;
        dialogueText.text = dialogue[0];
        GameManager.Instance.inConversation =  true;
    }
    
    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
        dialoguePanel.SetActive(false);
        GameManager.Instance.inConversation = false;
    }
}
