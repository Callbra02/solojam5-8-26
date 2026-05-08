using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private float moveSpeed;

    private Rigidbody2D _rb;

    public Vector2 wishMovement;
    private Vector2 _input;
    
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }
    
    private void HandleInput()
    {
        _input = _moveAction.action.ReadValue<Vector2>();

        if (_input != Vector2.zero)
            _input.Normalize();
    }

    private void HandleMovement()
    {
        wishMovement = _input * moveSpeed;
        
        _rb.linearVelocity = wishMovement * Time.fixedDeltaTime;
    }
}
