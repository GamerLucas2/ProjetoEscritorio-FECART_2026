using UnityEngine;

public class NPCscript : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private bool firstInteraction = true;
    [SerializeField] int repeatStartPosition;
    
    [Header("Bools")]
    public bool taskNPC;
    public bool hasDialogue;
    
    [Header("Used by DialogueController")]
    public string npcName;
    public int dialogueIndex;
    public DialogueAsset[] dialogueAsset;

    [HideInInspector]
    public int StartPosition 
    {
        get
        {
            if (dialogueIndex == 1)
            {
                return 0;
            }
            
            if (firstInteraction)
            {
                firstInteraction = false;
                return 0;
            }
            else
            {
                return repeatStartPosition;
            }
        }
    }

    private void Update()
    {
        if (taskNPC && GameManager.Instance.levelCompletable)
            dialogueIndex = 1;
        if (!taskNPC && TaskSystem.Instance.tasksActive)
            dialogueIndex = 1;
    }

    private void Start()
    {
        dialogueIndex = 0;
    }
}
