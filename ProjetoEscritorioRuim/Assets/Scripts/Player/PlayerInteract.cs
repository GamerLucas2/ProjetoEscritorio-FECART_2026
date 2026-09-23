using System;
using System.IO.Pipes;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    InputAction interact;
    InputAction swapItem;

    [Header("Raycast")]
    [SerializeField] LayerMask interactMask;
    [SerializeField] float maxDistance;
    RaycastHit hit;
    
    [Header("LoadPoint")]
    [SerializeField] private Transform loadPoint;
    [SerializeField] private Transform StoreTransform;
    [SerializeField] private GameObject[] storedItems =  new GameObject[2];
    [SerializeField] private int currentItem = 0;
    
    
    [Header("Other")]
    [SerializeField] private bool cantSwap;
    public ComputerTask computerTask;


    
    void Awake()
    {
        inputSystem = new InputSystem_Actions();
        interact = inputSystem.Player.Interact;
        swapItem = inputSystem.Player.SwapItem;
    }

    private void Start()
    {
        Ray cameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    }

    private void OnEnable()
    {
        inputSystem.Enable();

        DialogueController.OnDialogueStarted += JoinConversation;
        DialogueController.OnDialogueEnded += LeaveConversation;
    }

    private void OnDisable()
    {
        inputSystem.Disable();
        
        DialogueController.OnDialogueStarted -= JoinConversation;
        DialogueController.OnDialogueEnded -= LeaveConversation; ;
    }

    void Update()
    {
        if (interact.WasPressedThisFrame())
        {
            print("buttom Pressed");
            Interact();
        }
        
        if (swapItem.WasPressedThisFrame() && !cantSwap)
            SwapHeldItem();
    }

    #region Interaction Methods

    private void Interact() // Checks if the player is talking to an NPC or not
    {
        print("Interacting");
        if (GameManager.Instance.inConversation)
            DialogueController.Instance.SkipLine();
        else if (!GameManager.Instance.levelCleared && !PauseMenu.gameIsPaused)
            ItemInteraction();
    }
    
    private void ItemInteraction()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, interactMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Hit:  " + hit.transform.gameObject.name);
            
            CheckObjectType();
        }
    }

    private void CheckObjectType()
    {
        if (hit.transform.CompareTag("Item"))
        {
            Debug.Log("Interacted with Item");

            CheckIfCanPickUp();
        }
        else if (hit.transform.CompareTag("NPC"))
        {
            Debug.Log("Interacted with NPC");
            // Searches for the NPC, for dialogue and tasks
            GameManager.Instance.FindNPC(hit.transform);

            // Checks if the NPC is supposed to be given an item
            if (hit.transform.TryGetComponent(out PlaceScript placescript))
                PutItemDown();
        }
        else if (hit.transform.CompareTag("PutDown"))
        {
            Debug.Log("Interacted with PutDown");
            PutItemDown();
        }
        else if (hit.transform.CompareTag("Computer"))
        {
            Debug.Log("Interacted with Computer");
            computerTask.TaskWasinteracted(true);
        }
        else
        {
            Debug.Log("Interacted with None");
        }
    }

    private void CheckIfCanPickUp()
    {
        if (!GameManager.Instance.tasksActive) // Only pick up items if the level has been started and if can pick up
            print("Start the level to interact with object");
        else if (storedItems[currentItem] != null)
            print("Slot Full");
        else
            PickUpItem();
    }

    private void PickUpItem()
    {
        int i = currentItem;
        
        storedItems[i] = hit.transform.gameObject;
        UI_Manager.Instance.ToggleItemIndicator(i);
        storedItems[i].transform.position = loadPoint.position;
        
        storedItems[i].transform.parent = StoreTransform;
        
        UI_Manager.Instance.ChangeItemIndicatorState(i, Color.blue);
    }

    private void PutItemDown()
    {
        int i = currentItem;
        if (storedItems[i] != null)
        {
            hit.transform.TryGetComponent(out PlaceScript placeScript);
            storedItems[i].TryGetComponent(out ItemScript itemScript);
            
            if (!placeScript.hasItemOnTop)
            {
                UI_Manager.Instance.ToggleItemIndicator(i);
                storedItems[i].transform.position = hit.transform.Find("Display").transform.position + new Vector3(0, itemScript.displayPos, 0);
                storedItems[i].transform.parent = hit.transform;
                
                if (placeScript.hasTask)
                    GameManager.Instance.VerifyTaskID(storedItems[i], hit.transform.gameObject);
            
                storedItems[i] = null;
            }
            else
                print("Spot occupied");
        } 
    }


    private void SwapHeldItem()
    {
        if (currentItem == 1)
        {
            if (storedItems[currentItem] != null)
                storedItems[currentItem].SetActive(false);
            
            currentItem = 0;
            
            if (storedItems[currentItem] != null)
                storedItems[currentItem].SetActive(true);
            
            UI_Manager.Instance.ChangeItemIndicatorState(0, Color.blue);
            UI_Manager.Instance.ChangeItemIndicatorState(1, Color.white);
        }
        else if (currentItem == 0)
        {
            if (storedItems[currentItem] != null)
                storedItems[currentItem].SetActive(false);
            
            currentItem = 1;
            
            if (storedItems[currentItem] != null)
                storedItems[currentItem].SetActive(true);
            
            UI_Manager.Instance.ChangeItemIndicatorState(1, Color.blue);
            UI_Manager.Instance.ChangeItemIndicatorState(0, Color.white);
        }
    }

    private void JoinConversation()
    {
        GameManager.Instance.inConversation = true;
        Time.timeScale = 0;
    }

    private void LeaveConversation()
    {
        GameManager.Instance.inConversation = false;
        Time.timeScale = 1;
    }
    
}
    #endregion
