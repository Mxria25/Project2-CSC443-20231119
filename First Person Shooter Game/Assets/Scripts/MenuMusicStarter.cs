using UnityEngine;

public class MenuMusicStarter : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance?.PlayMenuMusic();
    }
}