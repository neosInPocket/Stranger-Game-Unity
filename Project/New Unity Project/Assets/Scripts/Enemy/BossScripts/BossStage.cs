using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

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

    public UnityEvent onBossAppear;
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
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            onBossAppear?.Invoke();
            player.gameObject.GetComponentInChildren<Light2D>().enabled = false;
            _bossComet.SetActive(true);

            _wall.SetActive(true);

            Destroy(_bossComet, 2f);
            
            CinemachineShake.instance.ShakeCamera(15f, 3f);
        }
    }
}
