using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : AbstractEnemy
{
    [Header("Секунды до удаления обьекта")]
    [SerializeField] private float _secondDestroyObject;

    [Header("Центр вращения обьекта")]
    [SerializeField] private GameObject _center;

    [Header("Ось вращения")]
    [SerializeField] private Vector3 _axis;

    protected override void Start()
    {
        base.Start();

        _healthBar.SetHealthValue(_currentHealth, MaxHealth);
    }

    protected void Update()
    {
        
        
    }

    public override IEnumerator Die()
    {
        if (GameManager.instance.onEnemyDeathCollBack != null)
        {
            GameManager.instance.onEnemyDeathCollBack.Invoke(enemyProfile);
        }

        yield return new WaitForSeconds(_secondDestroyObject);

        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        _currentHealth -= damage - _defence;

        _healthBar.SetHealthValue(_currentHealth, MaxHealth);

        if (_currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void RotateBossAround()
    {
        transform.RotateAround(_center.transform.position, _axis, 0.1f);
    }
}
