using UnityEngine;

public class GameplayMusicStarter : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance?.PlayGameplayMusic();
    }
}