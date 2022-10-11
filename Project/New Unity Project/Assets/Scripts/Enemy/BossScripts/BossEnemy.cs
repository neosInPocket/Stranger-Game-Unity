using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("Финальная стадия босса")]
    [SerializeField] private GameObject _bоssFinal;

    void StartFinalyBoss()
    {
        _bоssFinal.SetActive(true);
    }
    
}
