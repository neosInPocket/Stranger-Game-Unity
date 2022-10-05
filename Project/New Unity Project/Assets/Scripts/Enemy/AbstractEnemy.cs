  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    [Header("AbstractEnemy")]
    [SerializeField] public float MaxHealth;
    public float _moveSpeed;
    protected float _currentHealth;
    public EnemyProfile enemyProfile;

    public abstract void TakeDamage(float damage);

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }
}
