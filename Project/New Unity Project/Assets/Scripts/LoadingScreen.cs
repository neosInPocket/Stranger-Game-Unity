using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public string loadLevel;

    public GameObject loadingScreen;

    public Slider bar;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Load()
    {
        loadingScreen.SetActive(true);

        //SceneManager.LoadScene(loadLevel);

        StartCoroutine(LoadScreenAsync());
    }

    IEnumerator LoadScreenAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadLevel);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            bar.value = operation.progress;

            if (operation.progress >= .9f && !operation.allowSceneActivation)
            {
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
