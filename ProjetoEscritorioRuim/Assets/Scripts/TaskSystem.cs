using TMPro;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public float completedTasks=0f;
    private bool holding = false;
    [SerializeField] private string tasksLeft;
    [SerializeField] private TextMeshProUGUI taskNumber;
    
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
