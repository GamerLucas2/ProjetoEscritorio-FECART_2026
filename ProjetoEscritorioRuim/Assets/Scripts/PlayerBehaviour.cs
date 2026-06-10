using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour: MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    
    #region Movement Variables
    
    private InputAction move;
    private float velocityX;
    private float velocityY;
    
    [SerializeField] private float moveSpeed = 10f;
    
    [SerializeField] private Rigidbody rigidBody;
    
    #endregion
    
    private GameObject mainCamera;
    [SerializeField] private float rotationSpeed = 20f;
    private float rotationVelocity;
    
    private float theshold;
    // private Vector2 mousePosition;

    #region Main Methods
    
    private void Awake()
    {
        inputSystem = new InputSystem_Actions();
        rigidBody = GetComponent<Rigidbody>();
        move = inputSystem.Player.Move;
        
        mainCamera = GameObject.Find("Main Camera");
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
        SetVelocity();
    }

    private void LateUpdate()
    {
        // RotateCamera();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    #endregion

    #region Movement Methods
    
    private void SetVelocity()
    {
        velocityX = move.ReadValue<Vector2>().x;
        velocityY = move.ReadValue<Vector2>().y;
    }
    
    void MovePlayer()
    {
        rigidBody.linearVelocity = new Vector3(velocityX, 0f, velocityY) * moveSpeed;
    }
    
    #endregion


    /*private void RotateCamera()
    {
        // Get mouse pos
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        
        if (mousePosition != Vector2.zero)
        {
            rotationVelocity = mousePosition.x * rotationSpeed * 1f;
        
            transform.Rotate(Vector3.up, rotationVelocity);
            mainCamera.transform.Rotate(Vector3.right, rotationVelocity);
        }
        
    }*/
}
