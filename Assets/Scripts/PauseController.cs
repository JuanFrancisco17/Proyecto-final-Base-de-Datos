using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject _pausePanel;
    private bool _isGameRunning;
    void Start()
    {
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            _pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            _pausePanel.SetActive(false);
        }
    }
}
