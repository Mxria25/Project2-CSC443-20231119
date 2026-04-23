using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject howToPlayPanel;
    [SerializeField] GameObject loadingPanel;

    [SerializeField] string gameplaySceneName = "Map 2";
    [SerializeField] float loadingDelay = 2f;

    private bool isLoading = false;

    public void StartGame()
    {
        if (isLoading) return;
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        isLoading = true;

        if (mainMenuUI != null)
            mainMenuUI.SetActive(false);

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);

        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        yield return new WaitForSeconds(loadingDelay);

        SceneManager.LoadScene(gameplaySceneName);
    }

    public void ShowHowToPlay()
    {
        if (isLoading) return;

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
    }

    [SerializeField] UIFade howToPlayFade;

public void HideHowToPlay()
{
    if (isLoading) return;

    if (howToPlayFade != null)
    {
        howToPlayFade.FadeOutAndDisable();
    }
    else if (howToPlayPanel != null)
    {
        howToPlayPanel.SetActive(false);
    }
}
}