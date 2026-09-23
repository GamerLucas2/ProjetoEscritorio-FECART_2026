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
    [SerializeField] private GameObject typeTaskPanel;
    [SerializeField] private bool isTypeTask;
    [SerializeField] private TMP_InputField WriteSpace;
    [SerializeField] private string task2Awnser;
    public static bool isOnComputer { get; private set; }
    
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
        GameManager.DisableMovement?.Invoke();
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        computerUI.SetActive(true);
        isOnComputer = true;
        if (isTypeTask)
        {
            InitiateTypeTask();
        }
    }
    private void QuitComputer()
    {
        computerUI.SetActive(false);
        GameManager.EnableMovement?.Invoke();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isOnComputer = false;
    }
    void InitiateTypeTask()
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
