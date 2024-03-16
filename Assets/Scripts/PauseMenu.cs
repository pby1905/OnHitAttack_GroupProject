using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pausePanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    public void MenuLoad()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
