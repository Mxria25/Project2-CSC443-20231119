using System.Collections;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duration = 0.25f;

    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float t = 0f;
        canvasGroup.alpha = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = t / duration;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    public void FadeOutAndDisable()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        float t = 0f;
        float startAlpha = canvasGroup.alpha;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t / duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}