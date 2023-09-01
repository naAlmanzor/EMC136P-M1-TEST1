using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateScene : MonoBehaviour
{
    [SerializeField] private GameStats gameStats;
    [SerializeField] private TextMeshProUGUI gameOverText, clearText;
    [SerializeField] private GameObject retryButton, nextButton;

    void Start()
    {
        if(gameStats.levelCleared) {
            gameOverText.gameObject.SetActive(false);
            clearText.gameObject.SetActive(true);
            retryButton.SetActive(false);
            nextButton.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(gameStats.previousLevel);
    }

    public void Next()
    {
        if(SceneUtility.GetBuildIndexByScenePath(gameStats.previousLevel) != 4) {
            SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + gameStats.previousLevel + ".unity") + 1);
        }
        else Quit();
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
