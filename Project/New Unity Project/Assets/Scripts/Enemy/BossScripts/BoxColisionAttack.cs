using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColisionAttack : MonoBehaviour
{
    [Header("Обьект игрока")]
    [SerializeField] private Player _playerHealht;

    [Header("Урон врага")]
    [SerializeField] private float _damage;

    private void OnCollisionEnter2D(Collision2D collision)
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
