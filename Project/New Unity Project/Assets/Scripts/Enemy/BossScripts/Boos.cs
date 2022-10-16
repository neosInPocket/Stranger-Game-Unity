using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boos : AbstractEnemy
{
    [Header("����������� ������")]
    [SerializeField] private GameObject _object;

    [Header("������� �� �������� �������")]
    [SerializeField] private float _secondDestroyObject;

    [Header("����� ������")]
    [SerializeField] private GameObject _spawnEnemy;

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

    void SpawnEnemyTrue()
    {
        _spawnEnemy.SetActive(true);
    }

    void SpawnEnemyFalse()
    {
        _spawnEnemy.SetActive(false);
    }

    void SetScriptTrue()
    {
        _object.GetComponent<RotateAroundBossAttack>().enabled = true;
    }

    void SetScriptFalse()
    {
        _object.GetComponent<RotateAroundBossAttack>().enabled = false;
    }

    void StartRedLineTrue()
    {
        _redLine.SetActive(true);
    }

    void StartRedLineFalse()
    {
        _redLine.SetActive(false);
    }

}
