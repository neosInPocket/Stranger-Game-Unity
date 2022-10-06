using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AbstractEnemy
{
    protected override void Start()
    {
        base.Start();

        _healthBar.SetHealthValue(_currentHealth, MaxHealth);
    }

    public override IEnumerator Die()
    {
        if (GameManager.instance.onEnemyDeathCollBack != null)
        {
            GameManager.instance.onEnemyDeathCollBack.Invoke(enemyProfile);
        }

        yield return new WaitForSeconds(0f);

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
}
