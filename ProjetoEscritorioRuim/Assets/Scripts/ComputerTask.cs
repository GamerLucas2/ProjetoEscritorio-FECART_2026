using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerTask : MonoBehaviour
{
    public static ComputerTask instance { get; private set; }

    public bool computertaskComplete = false;
    private bool onComputer;

    InputSystem_Actions inputSystemActions;
    InputAction interact;
    float range;
    [SerializeField] private string taskID;
    [SerializeField] private GameObject completeTaskButton;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject buttonTaskPanel;
    [SerializeField] private bool isDrag, isButton;
    private void Awake()
    {
        inputSystemActions = GetComponent<InputSystem_Actions>();
        interact = inputSystemActions.Player.Interact;
        computerUI.SetActive(false);
        onComputer = false;
    }
    private void Update()
    {
        if(interact.WasPressedThisFrame())
        {
            if (onComputer == true)
            {
                ComputerInitialized();
            }
            else if (onComputer == false)
            {
                QuitComputer();
            }
        }
    }
    private void TaskWasinteracted(bool playerInteract)
    {
        if (playerInteract)
        {
            onComputer = true;
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
        computerUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (isButton == true)
        {
            ButtonTask();
        }
    }
    void ButtonTask()
    {
        buttonTaskPanel.SetActive(true);
    }
    public void CompleteComputer()
    {
        {
            QuitComputer();
            computertaskComplete = true;
        }
    }
}
