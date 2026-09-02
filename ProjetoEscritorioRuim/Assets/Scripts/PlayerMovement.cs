using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement: MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    
    #region Movement Variables
    // Input \\
    private InputAction move;
    
    private float horizontalInput;
    private float verticalInput;
    Vector3 moveDirection;
    
    // Movement \\
    [SerializeField] private float currentSpeed;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float damping;
    private float slowDown = 1f;
    #endregion

    #region References
    [Header("References")]
    
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Transform orientation;
    #endregion References
    
    #region Event Functions
    
    private void Awake()
    {
        inputSystem = new InputSystem_Actions();
        move = inputSystem.Player.Move;
        
        rigidBody = GetComponent<Rigidbody>();
        orientation = GameObject.Find("Orientation").transform;
    }

    void Start()
    {
        rigidBody.freezeRotation = true;
    }

    void Update()
    {
        GetInput();
        ApplyDamping();
        // limits max velocity to moveSpeed
        SpeedControl();
        
        currentSpeed = rigidBody.linearVelocity.magnitude;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.levelCleared)
            MovePlayer();
    }
    
    // Disable/Enable
    private void OnEnable()
    {
        inputSystem.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }
    
    
    #endregion

    #region Movement Methods
    private void GetInput()
    {
        horizontalInput = move.ReadValue<Vector2>().x;
        verticalInput = move.ReadValue<Vector2>().y;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            slowDown = 0.4f;
            rigidBody.linearVelocity *= 0.5f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            slowDown = 1f;
        }
    }
    void MovePlayer()
    {
        // movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        // Move player
        rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10 * slowDown, ForceMode.Force);
    }

    private void ApplyDamping()
    {
        rigidBody.linearDamping = damping;
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rigidBody.linearVelocity.x, 0f, rigidBody.linearVelocity.z);
        
        // Limit velocity if needed
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rigidBody.linearVelocity = new  Vector3(limitedVelocity.x, rigidBody.linearVelocity.y, limitedVelocity.z);
        }
    }
    #endregion
}
