  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    [Header("Здоровье врага")]
    [SerializeField] public float MaxHealth;

    [Header("Защита врага")]
    [SerializeField] public float _defence;

    [Header("Полоса здоровья врага")]
    [SerializeField] public HealthBar _healthBar;

    [Header("Профайл врага")]
    public EnemyProfile enemyProfile;

    protected float _currentHealth;

    public abstract void TakeDamage(float damage);

    public abstract IEnumerator Die();

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }
}
