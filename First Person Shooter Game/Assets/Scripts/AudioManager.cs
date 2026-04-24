using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private float musicFadeDuration = 1f;
    [SerializeField] private float musicVolume = 0.4f;

    [Header("UI SFX")]
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private float buttonClickVolume = 1f;

    [Header("Gameplay SFX")]
    [SerializeField] private AudioClip enemyDeathSFX;
    [SerializeField] private float enemyDeathVolume = 1f;

    [SerializeField] private AudioClip playerDeathSFX;
    [SerializeField] private float playerDeathVolume = 1f;

    [SerializeField] private AudioClip winSFX;
    [SerializeField] private float winVolume = 1f;

    [SerializeField] private AudioClip pickupSFX;
    [SerializeField] private float pickupVolume = 1f;

    [SerializeField] private AudioClip shopBuySFX;
    [SerializeField] private float shopBuyVolume = 1f;

    [SerializeField] private AudioClip notEnoughPointsSFX;
    [SerializeField] private float notEnoughPointsVolume = 1f;

    private Coroutine musicRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    private void PlayMusic(AudioClip newClip)
    {
        if (newClip == null || musicSource == null) return;

        if (musicSource.clip == newClip && musicSource.isPlaying) return;

        if (musicRoutine != null)
            StopCoroutine(musicRoutine);

        musicRoutine = StartCoroutine(FadeToMusic(newClip));
    }

    private IEnumerator FadeToMusic(AudioClip newClip)
    {
        float fadeDuration = Mathf.Max(0.01f, musicFadeDuration);
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0f)
        {
            musicSource.volume -= startVolume * Time.unscaledDeltaTime / fadeDuration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.volume = 0f;
        musicSource.Play();

        while (musicSource.volume < musicVolume)
        {
            musicSource.volume += musicVolume * Time.unscaledDeltaTime / fadeDuration;
            yield return null;
        }

        musicSource.volume = musicVolume;
    }

    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSFX, buttonClickVolume);
    }

    public void PlayEnemyDeath()
    {
        PlaySFX(enemyDeathSFX, enemyDeathVolume);
    }

    public void PlayPlayerDeath()
    {
        PlaySFX(playerDeathSFX, playerDeathVolume);
    }

    public void PlayWin()
    {
        PlaySFX(winSFX, winVolume);
    }

    public void PlayPickup()
    {
        PlaySFX(pickupSFX, pickupVolume);
    }

    public void PlayShopBuy()
    {
        PlaySFX(shopBuySFX, shopBuyVolume);
    }

    public void PlayNotEnoughPoints()
    {
        PlaySFX(notEnoughPointsSFX, notEnoughPointsVolume);
    }

    public void PlaySFX(AudioClip clip)
    {
        PlaySFX(clip, 1f);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.PlayOneShot(clip, volume);
    }
}