using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public float zOffset;

    public float zoomSpeed;

    public float minZoom;

    public float maxZoom;

    private InputActions inputActions;

    private float ZoomSize;

    private Camera _camera;

    private void Awake()
    {
        inputActions = new();
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Camera.Zoom.performed += OnCameraZoom;
        inputActions.Camera.Zoom.canceled += OnCameraZoom;
    }

    private void Update()
    {
        Zoom();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Camera.Zoom.performed -= OnCameraZoom;
        inputActions.Camera.Zoom.canceled -= OnCameraZoom;
    }

    private void OnCameraZoom(InputAction.CallbackContext context)
    {
        ZoomSize = context.ReadValue<Vector2>().y;
    }

    // 让相机视角跟随玩家探测器
    private void FollowPlayer()
    {
        transform.position = player.position + Vector3.forward * zOffset;
    }

    // 让玩家能缩放相机视野
    private void Zoom()
    {
        _camera.orthographicSize -= ZoomSize * zoomSpeed;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minZoom, maxZoom);
    }
}
