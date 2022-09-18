using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("My components/ScelletonLVL")]
public class LoadSceneTwoTrigger : MonoBehaviour
{
    [Header("Индекс сцены")]
    public int sceneIndex;

    void OnTriggerEnter2D(Collider myCollider)
    {
        if (myCollider.tag == ("EnemyScelleton"))
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}