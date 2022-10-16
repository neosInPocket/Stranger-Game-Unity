using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinFrame : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Caves");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
