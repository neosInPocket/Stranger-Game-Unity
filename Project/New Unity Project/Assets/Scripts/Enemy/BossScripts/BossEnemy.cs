using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("��������� ������ �����")]
    [SerializeField] private GameObject _b�ssFinal;

    void StartFinalyBoss()
    {
        _b�ssFinal.SetActive(true);
    }
    
}
