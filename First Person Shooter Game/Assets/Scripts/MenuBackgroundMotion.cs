using UnityEngine;

public class MenuBackgroundMotion : MonoBehaviour
{
    [SerializeField] private float moveAmount = 10f;
    [SerializeField] private float moveSpeed = 0.3f;

    private RectTransform rectTransform;
    private Vector2 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    private void Update()
    {
        float x = Mathf.Sin(Time.unscaledTime * moveSpeed) * moveAmount;
        float y = Mathf.Cos(Time.unscaledTime * moveSpeed * 0.7f) * moveAmount;

        rectTransform.anchoredPosition = startPosition + new Vector2(x, y);
    }
}