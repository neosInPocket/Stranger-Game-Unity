  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    [Header("�������� �����")]
    [SerializeField] public float MaxHealth;

    [Header("������ �����")]
    [SerializeField] public float _defence;

    [Header("������ �������� �����")]
    [SerializeField] public HealthBar _healthBar;

    [Header("������� �����")]
    public EnemyProfile enemyProfile;

    protected float _currentHealth;

    public abstract void TakeDamage(float damage);

    public abstract IEnumerator Die();

    protected virtual void Start()
    {
        _currentHealth = MaxHealth;
    }
}
