using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonAudioConnector : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(AttachToButtonsRoutine());
    }

    private IEnumerator AttachToButtonsRoutine()
    {
        while (true)
        {
            Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);

            foreach (Button button in buttons)
            {
                // Avoid adding duplicate listeners
                button.onClick.RemoveListener(PlayClick);
                button.onClick.AddListener(PlayClick);
            }

            yield return new WaitForSeconds(1f); // check every second
        }
    }

    private void PlayClick()
    {
        AudioManager.Instance?.PlayButtonClick();
    }
}