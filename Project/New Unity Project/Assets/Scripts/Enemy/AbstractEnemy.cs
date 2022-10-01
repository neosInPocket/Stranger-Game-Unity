  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    [Header("Çהמנמגüו גנאדא")]
    public float MaxHealth;
    protected float _currentHealth;

    public EnemyProfile enemyProfile;

    public abstract void TakeDamage(float damage);

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }
}
