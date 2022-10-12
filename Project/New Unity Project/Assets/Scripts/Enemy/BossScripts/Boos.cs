using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boos : AbstractEnemy
{
    [Header("Вращающийся обьект")]
    [SerializeField] private GameObject _object;

    [Header("Секунды до удаления обьекта")]
    [SerializeField] private float _secondDestroyObject;

    [Header("Обьект скелетов 1")]
    [SerializeField] private GameObject _sceletonObject_1;

    [Header("Обьект скелетов 1")]
    [SerializeField] private GameObject _sceletonObject_2;

    [Header("Красные линии")]
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
    
    void FixedUpdate()
    {
        if (_currentHealth == 99)
        {
            SetScriptTrue();
        }
        else if (_currentHealth == 80)
        {
            SetScriptFalse();
        }
        else if (_currentHealth == 79)
        {
            StartSceleton1True();
        }
        else if (_currentHealth == 60)
        {
            StartSceleton1False();
        }
        else if (_currentHealth == 59)
        {
            SetScriptTrue();

            StartRedLineTrue();
        }
        else if (_currentHealth == 40)
        {
            SetScriptFalse();

            StartRedLineFalse();
        }
        else if (_currentHealth == 39)
        {
            StartSceleton2True();

            SetScriptTrue();

            StartRedLineTrue();
        }
        else if (_currentHealth == 20)
        {
            StartSceleton2False();

            SetScriptFalse();

            StartRedLineFalse();
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
