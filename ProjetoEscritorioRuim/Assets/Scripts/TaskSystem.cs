using TMPro;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public static TaskSystem Instance { get; private set; }
    
    public int completedTasks = 0;
    public bool tasksActive = false;
    [SerializeField] public int tasksLeft;
    [SerializeField] private TextMeshProUGUI taskNumberText;
    
    // [SerializeField] private GameObject endTaskObject;


    private void Awake()
    {
        Instance = this;
        // endTaskObject.SetActive(false);
    }
    
    private void Update()
    {
        taskNumberText.text = string.Format("Tasks: {0}/{1}", completedTasks.ToString(), tasksLeft.ToString());
    }
    public void CompleteTask() // Adds 1 to the task counter when the task is complete
    {
        completedTasks++;
    }

    
    /*public void FindTaskEnd(int taskID)
    {
        if (taskID == 1)
        {
            print("Completed task 1");
            completedTasks++;
        }
        
        if (taskID == 2)
        {
            print("Completed task 2");
            completedTasks++;
        }
    }*/
}
