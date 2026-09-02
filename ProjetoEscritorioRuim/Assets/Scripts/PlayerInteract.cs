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

    [SerializeField] LayerMask interactMask;
    [SerializeField] float maxDistance;
    RaycastHit hit;

    private bool hasItem;
    private bool canPickUp = true;
    
    
    [SerializeField] private GameObject[] storedItems =  new GameObject[2];
    [SerializeField] private GameObject[] itemIndicator  = new GameObject[2];

    [SerializeField] private int currentItem = 0;
    
    void Awake()
    {
        inputSystem = new InputSystem_Actions();
        interact = inputSystem.Player.Interact;
        swapItem = inputSystem.Player.SwapItem;
    }

    private void Start()
    {
        itemIndicator[0].SetActive(false);
        itemIndicator[1].SetActive(false);
        Ray cameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }

    void Update()
    {
        if (interact.WasPressedThisFrame())
            Interact();
        
        if (swapItem.WasPressedThisFrame())
            SwapHeldItem();
    }

    private void FixedUpdate()
    {
        // Checks if the player has an item
        if (storedItems[0] == null &&  storedItems[1] == null)
            hasItem = false;

        // Checks if the player can pick up another item
        if (storedItems[currentItem] != null || !TaskSystem.Instance.tasksActive)
        {
            canPickUp = false;
        }
        else
            canPickUp = true;
    }

    #region Interaction Methods

    private void Interact() // Checks if the player is talking to an NPC or not
    {
        if (GameManager.Instance.inConversation)
            UI_Manager.Instance.EndDialogue();
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
        else
        {
            Debug.Log("Interacted with None");
        }
    }

    private void CheckIfCanPickUp()
    {
        if (canPickUp) // Only pick up items if the level has been started and if can pick up
            PickUpItem();
        else if (!TaskSystem.Instance.tasksActive)
            print("Start the level to interact with object");
        else if (storedItems[currentItem] != null)
            print("Slot Full");
    }

    private void PickUpItem()
    {
        hasItem = true;
        int i = currentItem;
        
        /*if (storedItems[0] != null) // Makes so the items are sent to slot 1 if slot 0 is full
            i = 1;*/
        
        storedItems[i] = hit.transform.gameObject;
        itemIndicator[i].SetActive(true);
        storedItems[i].transform.position = new Vector3(0, 1000, 0);
        
    }

    private void PutItemDown()
    {
        int i = currentItem;
        if (storedItems[i] != null)
        {
            itemIndicator[i].SetActive(false);
            GameManager.Instance.VerifyTaskID(storedItems[i], hit.transform.gameObject);
            storedItems[i].transform.position = hit.transform.Find("Display").transform.position;
            storedItems[i] = null;
        }
    }

    private void SwapHeldItem()
    {
        if (currentItem == 1)
            currentItem = 0;
        else if (currentItem == 0)
            currentItem = 1;
    }
    #endregion
}
