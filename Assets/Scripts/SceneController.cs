using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToRanking()
    {
        SceneManager.LoadScene("Ranking");
    }
}
