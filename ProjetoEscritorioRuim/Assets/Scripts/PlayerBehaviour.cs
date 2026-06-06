using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour: MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    
    #region Movement Variables
    /*private float inputDirectionX => inputSystem.Player.Move.ReadValue<Vector2>().x;
    private float inputDirectionY => inputSystem.Player.Move.ReadValue<Vector2>().y;
    private Vector2 moveDirection;*/
    private InputAction move;
    private float velocityX;
    private float velocityY;
    
    [SerializeField] private float moveSpeed = 10f;
    #endregion
    
    [SerializeField] private Rigidbody rigidBody;

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


    void Update()
    {
        // moveDirection = new Vector3(inputDirectionX, transform.position.y, inputDirectionY);
        velocityX = move.ReadValue<Vector2>().x;
        velocityY = move.ReadValue<Vector2>().y;
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector3(velocityX, 0f, velocityY) * moveSpeed;
    }
}
