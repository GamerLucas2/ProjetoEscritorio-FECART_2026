using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class TaskSystem : MonoBehaviour
{
    InputSystem_Actions inputSystemActions;
    InputAction interaction;
    private bool holding = false;
    
    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        interaction = inputSystemActions.Player.Interact;
        interaction.Enable();
    }
    private void OnTriggerEnter(Collider collider)
    //Algo não tá funcionando não sei o porquê-Felipe
    {
        bool interactionPressed = interaction.WasPressedThisFrame();
        if (collider.CompareTag("TaskStart") && interactionPressed)
        {
            print("Task Started");
            holding = true;
        }
        else if (collider.CompareTag("TaskEnd") && interactionPressed && holding)
        {
            print("Task Ended");
            holding = false;
        }
    }
}
