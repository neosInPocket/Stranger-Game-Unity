  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    [Header("Çהמנמגüו גנאדא")]
    public float MaxHealth = 100;
    protected float _currentHealth;

    public abstract void TakeDamage(float damage);

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }
}
