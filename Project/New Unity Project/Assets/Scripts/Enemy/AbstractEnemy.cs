  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    [Header("Çהמנמגüו גנאדא")]
    public int MaxHealth = 3;
    protected int _currentHealth;

    public abstract void TakeDamage(int damage);

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }
}
