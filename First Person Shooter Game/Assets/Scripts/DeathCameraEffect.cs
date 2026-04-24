using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DeathCameraEffect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Image redOverlay;

    [Header("Camera Tilt")]
    [SerializeField] private float tiltAngle = 90f;
    [SerializeField] private float tiltDuration = 1.2f;

    [Header("Red Fade")]
    [SerializeField] private float finalRedAlpha = 0.45f;
    [SerializeField] private float fadeDuration = 1.5f;

    private bool played;

    private void Start()
    {
        if (playerHealth == null)
            playerHealth = FindAnyObjectByType<PlayerHealth>();

        if (virtualCamera == null)
            virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();

        if (redOverlay != null)
        {
            Color c = redOverlay.color;
            c.a = 0f;
            redOverlay.color = c;
        }

        if (playerHealth != null)
            playerHealth.OnDied += PlayDeathEffect;
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnDied -= PlayDeathEffect;
    }

    private void PlayDeathEffect()
    {
        if (played) return;
        played = true;

        StartCoroutine(DeathEffectRoutine());
    }

    private IEnumerator DeathEffectRoutine()
    {
        float timer = 0f;

        float startDutch = virtualCamera != null ? virtualCamera.m_Lens.Dutch : 0f;
        float targetDutch = startDutch + tiltAngle;

        float totalDuration = Mathf.Max(tiltDuration, fadeDuration);

        while (timer < totalDuration)
        {
            timer += Time.unscaledDeltaTime;

            if (virtualCamera != null)
            {
                float tTilt = Mathf.Clamp01(timer / tiltDuration);
                tTilt = Mathf.SmoothStep(0f, 1f, tTilt);

                LensSettings lens = virtualCamera.m_Lens;
                lens.Dutch = Mathf.Lerp(startDutch, targetDutch, tTilt);
                virtualCamera.m_Lens = lens;
            }

            if (redOverlay != null)
            {
                float tFade = Mathf.Clamp01(timer / fadeDuration);
                Color c = redOverlay.color;
                c.a = Mathf.Lerp(0f, finalRedAlpha, tFade);
                redOverlay.color = c;
            }

            yield return null;
        }
    }
}