using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectButtonAfterInteraction : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Selectable buttonToSelect;
    
    [Header("Debug")]
    [SerializeField] private bool showVisualization;
    [SerializeField] private Color debugColor =  Color.cadetBlue;

    private void OnDrawGizmos() // Visualize the connection between the buttons
    {
        if (!showVisualization)
            return;
        
        if (buttonToSelect == null)
            return;
        
        Gizmos.color = debugColor;
        Gizmos.DrawLine(gameObject.transform.position, buttonToSelect.transform.position);
    }

    private void Reset() // Calls this method when the script is atached to a GameObject, or by clicking "Reset" on the context menu
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
        
        if (eventSystem == null)
            print("EventSystem not found in scene");
    }

    public void JumpToElement()
    {
        if (eventSystem == null)
            print("No EventSystem referenced");

        if (buttonToSelect == null)
            print("No ButtonToSelect referenced");

        eventSystem.SetSelectedGameObject(buttonToSelect.gameObject);
    }
}
