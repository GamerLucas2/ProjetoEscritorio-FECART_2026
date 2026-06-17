using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class TaskSystem : MonoBehaviour
{
    //InputSystem_Actions inputSystemActions;
    //InputAction interaction;
    public float completedTasks=0f;
    private bool holding = false;
    [SerializeField] private string tasksLeft;
    [SerializeField] private TextMeshProUGUI taskNumber;
/*private void Awake()
{
    inputSystemActions = new InputSystem_Actions();
    interaction = inputSystemActions.Player.Interact;
    interaction.Enable();
}*/
private void Update()
{
    taskNumber.text= "Tasks Done: "+completedTasks + tasksLeft;
}
private void OnTriggerEnter(Collider taskCollider)
{
    if (taskCollider.CompareTag("TaskStart") && !holding)
    {
        print("TaskStart");
        holding = true;
        Destroy(taskCollider.gameObject);
    }
    else if (taskCollider.CompareTag("TaskEnd") && holding)
    {
        print("TaskEnd");
        holding = false;
        taskCollider.gameObject.tag = "TaskComplete";
        completedTasks++;
    }
}
}
