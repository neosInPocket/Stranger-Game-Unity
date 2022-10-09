using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartAndExiteButtons : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("CoreWorld");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
