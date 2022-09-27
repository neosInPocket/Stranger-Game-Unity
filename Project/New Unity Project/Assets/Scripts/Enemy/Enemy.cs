using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AbstractEnemy
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float defence = 0;
    private bool _isHitting = false;

    protected override void Start()
    {
        base.Start();
        _healthBar.SetHealthValue(_currentHealth, MaxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<AbstractEnemy>() != null)
        {
            TakeDamage(GetComponent<PistolBullet>().Damage);
        }
    }

    private IEnumerator Die()
    {
        _isHitting = true;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        _currentHealth -= damage - defence;
        _healthBar.SetHealthValue(_currentHealth, MaxHealth);
        if (_currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }
}
