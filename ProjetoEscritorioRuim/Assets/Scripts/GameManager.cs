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
            if (NPCScript.hasTask && !NPCScript.taskCompleted)
            {
                taskNumber = NPCScript.TaskID;
                NPCScript.taskCompleted = true;
                TaskSystem.Instance.FindTaskEnd(taskNumber);
            }
        }
    }
}
