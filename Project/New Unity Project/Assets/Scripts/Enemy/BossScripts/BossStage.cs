using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossStage : MonoBehaviour
{
    [Header("������ ������")]
    [SerializeField] private GameObject _bossComet;

    [Header("��������� �����")]
    [SerializeField] private GameObject _bossApearanse;

    

    [Header("������ ���� ������")]
    [SerializeField] private GameObject _bossTrigger;

    [Header("����� ����� ����� �� �����")]
    [SerializeField] private GameObject _wall;

    void Update()
    {
        if (_bossApearanse != null)
        {
            if (_bossComet == null)
            {
                _bossApearanse.SetActive(true);

                Destroy(_bossApearanse, 3.1f);

                Destroy(_bossTrigger, 3f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            _bossComet.SetActive(true);

            _wall.SetActive(true);

            Destroy(_bossComet, 2f);
        }
    }
}
