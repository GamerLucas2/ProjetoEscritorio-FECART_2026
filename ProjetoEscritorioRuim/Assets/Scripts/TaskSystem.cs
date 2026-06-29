using TMPro;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public static TaskSystem Instance { get; private set; }
    
    public float completedTasks = 0f;
    private bool taskActive = false;
    [SerializeField] private string tasksLeft;
    [SerializeField] private TextMeshProUGUI taskNumberText;
    
    [SerializeField] private GameObject endTaskObject;


    private void Awake()
    {
        Instance = this;
        endTaskObject.SetActive(false);
    }
    
    private void Update()
    {
        taskNumberText.text= "Tasks Done: "+completedTasks + tasksLeft;
    }


    public void FindTaskStart(string taskName)
    {
        if (taskName == "Task 1")
        {
            endTaskObject.SetActive(true);
            print("Started task 1");
            taskActive = true;
            
            
        }
        else if (taskName == "Task 2")
        {
            
        }
    }

    public void FindTaskEnd(string taskName)
    {
        if (taskName == "Task 1")
        {
            print("Completed task 1");
            taskActive = false;
            completedTasks++;
        }
    }
}
