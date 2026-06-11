using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    float xSensitivity;
    float ySensitivity;
    
    public Transform orientation;
    
    float xRotation;
    float yRotation;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;
    }
    
    void Update()
    {
        //* Mouse Input
        float mouseX = Mouse.current.position.ReadValue().x * Time.deltaTime * xSensitivity;
        float mouseY = Mouse.current.position.ReadValue().y * Time.deltaTime * ySensitivity;
        
        yRotation += mouseX;
        xRotation -= mouseY;
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        //* Rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
