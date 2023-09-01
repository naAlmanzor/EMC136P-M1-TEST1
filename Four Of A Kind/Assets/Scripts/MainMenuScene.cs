using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour
{
    public void BeginGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit(0);
    }
}
