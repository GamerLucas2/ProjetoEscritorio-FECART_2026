using System;
using System.IO.Pipes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    InputAction interact;
    

    [SerializeField] LayerMask interactMask;
    [SerializeField] float maxDistance;
    RaycastHit hit;
    
    bool hasItem = false;
    [SerializeField] private GameObject currentItem;
    [SerializeField] private GameObject itemIndicator;
    
    
    void Awake()
    {
        inputSystem = new InputSystem_Actions();
        interact = inputSystem.Player.Interact;
    }

    private void Start()
    {
        itemIndicator.SetActive(false);
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
            InteractWithItem();
    }

    #region MyMethods

    private void InteractWithItem()
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
        if(hit.transform.CompareTag("Item"))
        {
            Debug.Log("Interacted with Item");
            PickUpItem();
        }
        else if (hit.transform.CompareTag("NPC"))
        {
            Debug.Log("Interacted with NPC");
            
            // Searches for the NPC, for dialogue and tasks
            GameManager.Instance.FindNPC(hit.transform);
        }
        else if (hit.transform.CompareTag("PutDown"))
        {
            Debug.Log("Interacted with PutDown");
            PutItemDown(currentItem);
        }
        else
        {
            Debug.Log("Interacted with None");
        }
    }

    private void PickUpItem()
    {
        hasItem = true;
        currentItem = hit.transform.gameObject;
        itemIndicator.SetActive(true);
        
        currentItem.transform.position = new Vector3(0, 1000, 0);
    }

    private void PutItemDown(GameObject item)
    {
        if (hasItem)
        {
            hasItem = false;
            itemIndicator.SetActive(false);
            GameManager.Instance.VerifyTaskID(item, hit.transform.gameObject);
            item.transform.position = hit.transform.Find("Display").transform.position;
            
            currentItem = null;
        }
    }
    #endregion
}
