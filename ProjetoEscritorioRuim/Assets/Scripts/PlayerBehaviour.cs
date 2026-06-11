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
    

    #region Main Methods
    
    private void Awake()
    {
        inputSystem = new InputSystem_Actions();
        rigidBody = GetComponent<Rigidbody>();
        move = inputSystem.Player.Move;
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
}
