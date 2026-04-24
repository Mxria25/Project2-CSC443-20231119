using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float height = 35f;

    private void LateUpdate()
    {
        if (player == null) return;

        transform.position = new Vector3(player.position.x, height, player.position.z);
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}