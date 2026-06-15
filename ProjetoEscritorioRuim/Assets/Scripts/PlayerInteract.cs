using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    LayerMask interactMask;

    private InputSystem_Actions inputSystem;
    private InputAction interact;
    
    void Awake()
    {
        inputSystem = new InputSystem_Actions();
        interact = inputSystem.Player.Interact;
        
        interactMask = LayerMask.GetMask("Interact");
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
        if (interact.WasPressedThisFrame())
            InteractWithItem();
    }

    private void InteractWithItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, interactMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Hit:  " + hit.transform.gameObject.name);
        }
    }
}
