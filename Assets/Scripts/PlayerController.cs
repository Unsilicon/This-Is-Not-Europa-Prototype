using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private InputActions inputActions;

    private Vector2 moveDirection;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        inputActions = new();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnPlayerMove;
        inputActions.Player.Move.canceled += OnPlayerMove;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Move.performed -= OnPlayerMove;
        inputActions.Player.Move.canceled -= OnPlayerMove;
    }

    private void OnPlayerMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    // 让玩家能操控探测器移动
    private void Move()
    {
        Vector3 direction = moveDirection.normalized * (moveSpeed * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(transform.position + direction);
    }
}
