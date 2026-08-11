using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public bool talkingToNPC = false;
    
    private bool levelCompletable = false;
    public bool levelCleared = false;

    public int taskNumber;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        Instance = this;
        
        levelCompletable = false;
        levelCleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(TaskSystem.Instance.completedTasks >= TaskSystem.Instance.tasksLeft)
        {
            TaskSystem.Instance.completedTasks = TaskSystem.Instance.tasksLeft;
            levelCompletable = true;
        }
    }
    
    
    // Also use this for finding the correct dialogue later
    public void FindNPC(Transform NPC)
    {
        if (NPC.TryGetComponent(out NPCscript NPCScript))
        {
            if (NPCScript.taskNPC && !TaskSystem.Instance.tasksActive)
            {
                TaskSystem.Instance.tasksActive = true;
                print ("Tasks Activated");
            }
            else if (NPCScript.taskNPC && levelCompletable)
            {
                print("Level Completed");
                UI_Manager.Instance.EndLevelScreen();
                levelCleared = true;
            }
        }
        
        
    }

    public void VerifyTaskID(GameObject currentItem, GameObject itemPlaceObject) // This verifies if the item and place position have the same TaskID
    {
        if (currentItem.TryGetComponent(out ItemScript ItemScript) && itemPlaceObject.TryGetComponent(out PlaceScript PlaceScript))
        {
            print("Got script");
            if (ItemScript.TaskID == PlaceScript.TaskID && !ItemScript.hasBeenUsed) // If they do, then complete the task
            {
                TaskSystem.Instance.CompleteTask();
                print("Task Complete");
                ItemScript.hasBeenUsed = true;
            }
        }
    }
}
