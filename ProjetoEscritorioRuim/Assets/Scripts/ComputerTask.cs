using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerTask : MonoBehaviour
{
    public bool computertaskComplete = false;
    private bool onComputer;
    InputSystem_Actions inputSystemActions;
    InputAction interact;
    [SerializeField] private Transform playerPos;
    [SerializeField] private string taskID;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject task1Panel, task2Panel;
    [SerializeField] private bool isTask1, isTask2;
    private void Awake()
    {
        inputSystemActions = GetComponent<InputSystem_Actions>();
        interact = inputSystemActions.Player.Interact;
        computerUI.SetActive(false);
        onComputer = false;
    }
    private void Update()
    {
        if (onComputer)
        {
            ComputerInitialized();
        }
    }
    /*private bool PlayerInRange()
    {
        if ()
        {

        }
        else
        {

        }
    }*/
    private void TaskWasinteracted(bool playerInteract)
    {
        if (playerInteract)
        {
            onComputer = true;
            ComputerInitialized();
        }
    }
    private void QuitComputer()
    {
        onComputer = false;
        computerUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void ComputerInitialized()
    {
        computerUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (isTask1)
        {
            InitiateTask1();
        }
        else if (isTask2)
        {
            InitiateTask2();
        }
    }
    void InitiateTask1()
    {
        task1Panel.SetActive(true);
    }
    void InitiateTask2()
    {
        task2Panel.SetActive(true);
    }
    public void CompleteComputer()
    {
        {
            QuitComputer();
            computertaskComplete = true;
        }
    }
}
