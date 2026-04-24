using UnityEngine;

public class MinimapMarker : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private RectTransform mapRect;
    [SerializeField] private float mapSize = 100f;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 pos = target.position;

        float x = pos.x / mapSize;
        float z = pos.z / mapSize;

        rect.anchoredPosition = new Vector2(x * mapRect.rect.width, z * mapRect.rect.height);
    }
}