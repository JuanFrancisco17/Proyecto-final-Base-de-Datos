using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("SINGLETON")]

    private static GameOverManager instance;

    public static GameOverManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameOverManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private GameObject gameOverPanel;
    void Start()
    {
        gameOverPanel.SetActive(false);
    }


    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }
}
