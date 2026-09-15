using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerTask : MonoBehaviour
{
    public bool computerTaskComplete = false;
    public bool inRange;
    InputSystem_Actions inputSystemActions;
    InputAction interact;
    [SerializeField] private string taskID;
    [SerializeField] private GameObject computer;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject buttonTaskPanel, typeTaskPanel;
    [SerializeField] private bool isButtonTask, isTypeTask;
    [SerializeField] private TMP_InputField WriteSpace;
    [SerializeField] private string task2Awnser;
    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        interact = inputSystemActions.Player.Interact;
        computerUI.SetActive(false);
    }
    public void TaskWasinteracted(bool playerInteract)
    {
        if (playerInteract && GameManager.Instance.tasksActive == true)
        {
            ComputerInitialized();
        }
    }
    void ComputerInitialized()
    {
        Debug.Log("Computer Task Started");
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        computerUI.SetActive(true);
        if (isButtonTask)
        {
            InitiateTask1();
        }
        else if (isTypeTask)
        {
            InitiateTask2();
        }
    }
    private void QuitComputer()
    {
        computerUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void InitiateTask1()
    {
        buttonTaskPanel.SetActive(true);
    }
    void InitiateTask2()
    {
        typeTaskPanel.SetActive(true);
        string playerTxt = WriteSpace.text;
        if (playerTxt == task2Awnser)
        {
            CompleteComputer();
            computerTaskComplete = true;
        }
    }
    public void CompleteComputer()
    {
        QuitComputer();
        CompleteTaskOnTheThing();
        computerTaskComplete = true;
        computer.layer = LayerMask.NameToLayer("Default");
    }

    public void CompleteTaskOnTheThing()
    {
        Debug.Log("Task Completed");
        TaskSystem.Instance.CompleteTask();
        UI_Manager.Instance.CheckTaskInList(Convert.ToInt32(taskID));
    }
}
