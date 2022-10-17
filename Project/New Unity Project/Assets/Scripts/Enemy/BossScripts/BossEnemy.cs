using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("Что-то там")]
    [SerializeField] private GameObject _bossFinal;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void StartFinalyBoss()
    {
        audioSource.Play();
        _bossFinal.SetActive(true);

      
    }
   

}
