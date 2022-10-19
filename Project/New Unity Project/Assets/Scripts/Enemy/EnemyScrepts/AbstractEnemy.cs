  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    public float MaxHealth;

    public float _defence;

    public HealthBar _healthBar;

    public EnemyProfile enemyProfile;

    public Animator enemyAnimator;

    public new Collider2D collider2D;

    protected float _currentHealth;

    public abstract void TakeDamage(float damage);

    public abstract IEnumerator Die();

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }
}
