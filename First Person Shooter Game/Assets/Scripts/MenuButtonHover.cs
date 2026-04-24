using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scale")]
    [SerializeField] private float hoverScale = 1.08f;
    [SerializeField] private float speed = 10f;

    [Header("Text Color")]
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.cyan;

    private Vector3 normalScale;
    private Vector3 targetScale;

    private void Awake()
    {
        normalScale = transform.localScale;
        targetScale = normalScale;

        if (buttonText != null)
            buttonText.color = normalColor;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed * Time.unscaledDeltaTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = normalScale * hoverScale;

        if (buttonText != null)
            buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = normalScale;

        if (buttonText != null)
            buttonText.color = normalColor;
    }
}