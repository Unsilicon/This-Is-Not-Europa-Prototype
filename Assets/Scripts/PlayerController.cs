using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private InputActions inputActions;

    private Vector2 moveDirection;

    private Vector2 mousePosition;

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
        inputActions.Player.LookAtMouse.performed += OnPlayerLookAtMouse;
    }

    private void FixedUpdate()
    {
        Move();
        LookAtMouse();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Move.performed -= OnPlayerMove;
        inputActions.Player.Move.canceled -= OnPlayerMove;
        inputActions.Player.LookAtMouse.performed -= OnPlayerLookAtMouse;
    }

    private void OnPlayerMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void OnPlayerLookAtMouse(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    // 让玩家能操控探测器移动
    private void Move()
    {
        Vector2 direction = moveDirection.normalized * (moveSpeed * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(_rigidbody2D.position + direction);
    }

    // 让探测器看向鼠标方向
    private void LookAtMouse()
    {
        Vector2 direction = _rigidbody2D.position - (Vector2)Camera.main.ScreenToWorldPoint(mousePosition);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rigidbody2D.MoveRotation(angle);
    }
}
