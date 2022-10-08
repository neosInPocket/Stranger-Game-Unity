  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    [Header("Здоровье врага")]
    public float MaxHealth;

    [Header("Защита врага")]
    public float _defence;

    [Header("Полоса здоровья врага")]
    public HealthBar _healthBar;

    [Header("Профайл врага")]
    public EnemyProfile enemyProfile;

    [Header("Аниматор врага")]
    public Animator enemyAnimator;

    [Header("Collider2D для отключения")]
    public new Collider2D collider2D;

    protected float _currentHealth;

    public abstract void TakeDamage(float damage);

    public abstract IEnumerator Die();

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }
}
