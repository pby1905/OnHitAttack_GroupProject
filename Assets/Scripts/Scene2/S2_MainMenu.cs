using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S2_MainMenu : MonoBehaviour
{
    public void ExitLevel()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
