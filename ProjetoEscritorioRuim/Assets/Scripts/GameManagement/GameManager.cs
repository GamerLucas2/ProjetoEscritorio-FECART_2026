using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    
    public bool levelCompletable = false;
    public bool levelCleared = false;
    public bool inConversation;
    public bool tasksActive = false;

    public int taskNumber;

    public delegate void OnEnableMovement();
    public static OnEnableMovement EnableMovement;
    
    public delegate void OnDisableMovement();
    public static OnDisableMovement DisableMovement;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
        
        levelCompletable = false;
        levelCleared = false;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void OnEnable()
    {
        if (TaskSystem.Instance != null)
            TaskSystem.AllTasksComplete += AllTasksComplete;
        
        EnableMovement?.Invoke();
    }

    void OnDisable()
    {
        TaskSystem.AllTasksComplete -= AllTasksComplete; 
    }
    
    
    // Also use this for finding the correct dialogue later
    public void FindNPC(Transform NPC)
    {
        if(NPC.TryGetComponent(out PlaceScript placeScript ))
        {
            
        }
        
        if (NPC.TryGetComponent(out NPCscript npc))
        {
            
            if (npc.taskNPC && !tasksActive)
            {
                tasksActive = true;
                print ("Tasks Activated");
            }
            else if (npc.taskNPC && levelCompletable)
            {
                print("Level Completed");
                levelCleared = true;
                Time.timeScale = 0f;
                DialogueController.OnDialogueEnded += UI_Manager.Instance.EndLevelScreen;
            }
            if (npc.hasDialogue)
            {
                // UI_Manager.Instance.ShowDialogue(npc.dialogueAsset.dialogue, npc.name, npc.taskNPC);
                int index = npc.dialogueIndex;
                DialogueController.Instance.StartDialogue(npc.dialogueAsset[index].dialogue, npc.StartPosition, npc.npcName);
            }
            if(npc.taskNPC && !tasksActive)
                npc.ShowTextIfTaskNPC();

            if (npc.isFinalNPC && GameEndScript.Instance != null)
                DisableMovement?.Invoke();
        }
    }

    public void VerifyTaskID(GameObject currentItem, GameObject itemPlaceObject) // This verifies if the item and place position have the same TaskID
    {
        if (currentItem.TryGetComponent(out ItemScript itemScript) && itemPlaceObject.TryGetComponent(out PlaceScript placeScript))
        {
            print("Got script");
            if (itemScript.TaskID == placeScript.TaskID && !itemScript.hasBeenUsed) // If they do, then complete the task
            {
                TaskSystem.Instance.CompleteTask();
                UI_Manager.Instance.CheckTaskInList(Convert.ToInt32(itemScript.TaskID));
                print("Task Complete");
                itemScript.hasBeenUsed = true;
            }
        }
    }
    
    private void AllTasksComplete()
    {
        levelCompletable = true;
    }
}
