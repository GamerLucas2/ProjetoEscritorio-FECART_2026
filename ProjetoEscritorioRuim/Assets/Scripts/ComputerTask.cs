using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerTask : MonoBehaviour
{
    public bool computertaskComplete = false;
    private bool inRange;
    InputSystem_Actions inputSystemActions;
    InputAction interact;
    [SerializeField] private string taskID;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject task1Panel, task2Panel;
    [SerializeField] private bool isTask1, isTask2;
    [SerializeField] private TMP_InputField WriteSpace;
    [SerializeField] private Collider computerCollider;
    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        interact = inputSystemActions.Player.Interact;
        computerUI.SetActive(false);
    }
    private void Update()
    {
        if (inRange && interact.WasPressedThisFrame())
        {
            TaskWasinteracted(true);
        }
    }
    private void TaskWasinteracted(bool playerInteract)
    {
        if (playerInteract)
        {
            ComputerInitialized();
        }
    }
    private void QuitComputer()
    {
        computerUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void ComputerInitialized()
    {
        Debug.Log("Task Started");
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
        string playerTxt = WriteSpace.text;
        if (playerTxt == "Complete")
        {
            QuitComputer();
            computertaskComplete = true;
        }
    }
    public void CompleteComputer()
    {
        {
            QuitComputer();
            computertaskComplete = true;
        }
    }
}
