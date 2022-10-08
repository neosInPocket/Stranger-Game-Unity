  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    [Header("�������� �����")]
    public float MaxHealth;

    [Header("������ �����")]
    public float _defence;

    [Header("������ �������� �����")]
    public HealthBar _healthBar;

    [Header("������� �����")]
    public EnemyProfile enemyProfile;

    [Header("�������� �����")]
    public Animator enemyAnimator;

    [Header("Collider2D ��� ����������")]
    public new Collider2D collider2D;

    protected float _currentHealth;

    public abstract void TakeDamage(float damage);

    public abstract IEnumerator Die();

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }
}
