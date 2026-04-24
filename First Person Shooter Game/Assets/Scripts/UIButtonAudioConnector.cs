using UnityEngine;
using UnityEngine.UI;

public class UIButtonAudioConnector : MonoBehaviour
{
    private void Start()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance?.PlayButtonClick();
            });
        }
    }
}