using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    
    [Header("Camera Settings")]
    [SerializeField] float xSensitivity;
    [SerializeField] float ySensitivity;
    
    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform cameraPosition;
    
    InputSystem_Actions inputSystem;
    InputAction look;
    
    float xRotation;
    float yRotation;

    private void Awake()
    {
        orientation =  GameObject.Find("Orientation").transform;
        cameraPosition = GameObject.Find("CameraPos").transform;
        
        // Look action setup
        inputSystem = new InputSystem_Actions();
        look = inputSystem.Player.Look;
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;
    }
    
    void Update()
    {
        // Mouse Input
        float mouseX = look.ReadValue<Vector2>().x * Time.deltaTime * xSensitivity;
        float mouseY = look.ReadValue<Vector2>().y * Time.deltaTime * ySensitivity;
        
        yRotation += mouseX;
        xRotation -= mouseY;
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        // Rotate camera and orientation
        if (!GameManager.Instance.levelCleared)
        {
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        
        // Move camera with player
        MoveCamera();
    }
    
    private void OnEnable()
    {
        inputSystem.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }

    private void MoveCamera()
    {
        transform.position = cameraPosition.position;
    }
}
