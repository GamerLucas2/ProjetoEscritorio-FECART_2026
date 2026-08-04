using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public bool talkingToNPC = false;

    public int taskNumber;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    // Also use this for finding the correct dialogue later
    public void FindNPC(Transform NPC)
    {
        if (NPC.TryGetComponent(out NPCscript NPCScript))
        {
            /*if (NPCScript.hasTask)
            {
                taskNumber = NPCScript.TaskID;
                TaskSystem.Instance.FindTaskEnd(taskNumber);
            }*/
        }
        
        
    }

    public void VerifyTaskID(GameObject currentItem, GameObject itemPlaceObject) // This verifies if the item and place position have the same TaskID
    {
        if (currentItem.TryGetComponent(out ItemScript ItemScript) &&
            itemPlaceObject.TryGetComponent(out PlaceScript PlaceScript))
        {
            print("Got script");
            if (ItemScript.TaskID == PlaceScript.TaskID && !ItemScript.hasBeenUsed)
            {
                TaskSystem.Instance.CompleteTask();
                print("Task Complete");
                ItemScript.hasBeenUsed = true;
            }
        }
    }
}
