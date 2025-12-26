using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public float zOffset;

    private void LateUpdate()
    {
        FollowPlayer();
    }

    // 让相机视角跟随玩家探测器
    private void FollowPlayer()
    {
        transform.position = player.position + Vector3.forward * zOffset;
    }
}
