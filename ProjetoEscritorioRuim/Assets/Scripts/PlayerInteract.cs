using System;
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
    
    bool isInteracting = false;
    [SerializeField] private GameObject itemIndicator;
    [SerializeField] private GameObject interactObject;
    bool objectIsPresent = false;
    
    
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
        
    }

    private void FixedUpdate()
    {
        if (interact.WasPressedThisFrame() && !isInteracting)
        {
            isInteracting = true;
            InteractWithItem();
            if (interactObject.CompareTag("Item"))
            {
                itemIndicator.SetActive(true);
                interactObject.transform.position = new Vector3(0, 100000, 0);
            }
        }
        else if (interact.WasPressedThisFrame() && isInteracting)
        {
            PlaceObjBack();
            if (objectIsPresent)
            {
                isInteracting = false;
                itemIndicator.SetActive(false);
                
            }
        }
    }

    private void InteractWithItem()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, interactMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Hit:  " + hit.transform.gameObject.name);

            interactObject = hit.transform.gameObject;
        }
    }

    void PlaceObjBack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, interactMask))
            if (hit.transform.CompareTag("Place"))
            {
                interactObject.transform.position = hit.point;
                interactObject = null;
                objectIsPresent = true;
            }
            else
                objectIsPresent = false;
    }
}
