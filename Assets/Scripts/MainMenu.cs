using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class MainMenu : MonoBehaviour
{
    

    public void Playgame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ExitLevel()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
