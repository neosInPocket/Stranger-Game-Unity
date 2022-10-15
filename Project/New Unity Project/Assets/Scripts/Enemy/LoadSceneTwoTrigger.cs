using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("My components/ScelletonLVL")]
public class LoadSceneTwoTrigger : LoadingScreen
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Load();
    }
}
