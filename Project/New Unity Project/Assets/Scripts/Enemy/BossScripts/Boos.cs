using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boos : AbstractEnemy
{
    [Header("����������� ������")]
    [SerializeField] private GameObject _object;

    [Header("������� �� �������� �������")]
    [SerializeField] private float _secondDestroyObject;

    [Header("������ �������� 1")]
    [SerializeField] private GameObject _sceletonObject_1;

    [Header("������ �������� 1")]
    [SerializeField] private GameObject _sceletonObject_2;

    [Header("������� �����")]
    [SerializeField] private GameObject _redLine;

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

    void SetScriptTrue()
    {
        _object.GetComponent<RotateAroundBossAttack>().enabled = true;
    }

    void SetScriptFalse()
    {
        _object.GetComponent<RotateAroundBossAttack>().enabled = false;
    }

    void StartSceleton1True()
    {
        _sceletonObject_1.SetActive(true);
    }

    void StartSceleton1False()
    {
        _sceletonObject_1.SetActive(false);
    }

    void StartRedLineTrue()
    {
        _redLine.SetActive(true);
    }

    void StartRedLineFalse()
    {
        _redLine.SetActive(false);
    }

    void StartSceleton2True()
    {
        _sceletonObject_2.SetActive(true);
    }

    void StartSceleton2False()
    {
        _sceletonObject_2.SetActive(false);
    }
}
