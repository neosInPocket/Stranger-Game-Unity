using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("Что-то там")]
    [SerializeField] private GameObject _bossFinal;

    void StartFinalyBoss()
    {
        _bossFinal.SetActive(true);
    }
    
}
