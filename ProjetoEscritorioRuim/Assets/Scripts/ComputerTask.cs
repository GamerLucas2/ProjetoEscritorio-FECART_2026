using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerTask : MonoBehaviour
{
    public bool computertaskComplete = false;
    public bool inRange;
    InputSystem_Actions inputSystemActions;
    InputAction interact;
    [SerializeField] private string taskID;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject task1Panel, task2Panel;
    [SerializeField] private bool isTask1, isTask2;
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
        if (playerInteract)
        {
            ComputerInitialized();
        }
    }
    void ComputerInitialized()
    {
        Time.timeScale = 0f;
        Debug.Log("Computer Task Started");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        computerUI.SetActive(true);
        if (isTask1)
        {
            InitiateTask1();
        }
        else if (isTask2)
        {
            InitiateTask2();
        }
    }
    private void QuitComputer()
    {
        Time.timeScale = 1f;
        computerUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void InitiateTask1()
    {
        task1Panel.SetActive(true);
    }
    void InitiateTask2()
    {
        task2Panel.SetActive(true);
        string playerTxt = WriteSpace.text;
        if (playerTxt == task2Awnser)
        {
            Debug.Log("Task Completed");
            QuitComputer();
            computertaskComplete = true;
        }
    }
    public void CompleteComputer()
    {
        {
            Debug.Log("Task Completed");
            QuitComputer();
            computertaskComplete = true;
        }
    }
}
