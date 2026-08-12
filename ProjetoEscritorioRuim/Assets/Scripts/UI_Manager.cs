using System;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject endScreen;
    
    [SerializeField] private TextMeshProUGUI[] taskNameText = new TextMeshProUGUI[2];
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndLevelScreen()
    {
        endScreen.SetActive(true);
    }

    public void CheckTaskInList(int taskNumber)
    {
        taskNameText[taskNumber].text = "Complete";
    }
}
