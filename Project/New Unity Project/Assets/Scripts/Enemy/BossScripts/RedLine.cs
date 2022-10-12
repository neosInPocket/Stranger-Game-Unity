using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLine : MonoBehaviour
{
    [Header("������ ������")]
    [SerializeField] private Player _playerHealht;

    [Header("���� �����")]
    [SerializeField] private float _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            DamagePlayer();
        }
    }

    public void DamagePlayer()
    {
        _playerHealht.GetDamage(_damage);
    }
}
