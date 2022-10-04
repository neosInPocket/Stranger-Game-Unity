using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("My components/ScelletonLVL")]
public class LoadSceneTwoTrigger : LoadingScreen
{
    //[Header("Индекс сцены")]
    //public int sceneIndex;

    //private void OnCollisionEnter2D(Collision2D myCollider)
    //{
    //    if (myCollider.gameObject.name == "Player")
    //    {
    //        SceneManager.LoadScene(sceneIndex);
    //    }
    //}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Load();
    }
}
