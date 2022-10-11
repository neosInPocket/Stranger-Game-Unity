using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AbstractEnemy
{
    [Header("Секунды до удаления обьекта")]
    [SerializeField] private float _secondDestroyObject;

    [Header("Скорость врага")]
    [SerializeField] private float _speed;

    [Header("Точка патрулирования врага")]
    [SerializeField] private Transform _point;

    [Header("Растояние на которое враг будет отдаляться от точки патрулирования")]
    [SerializeField] private int _positionOfPatrol;

    [Header("Sprite врага")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Обьект игрока")]
    [SerializeField] private Transform _player;

    [Header("Обьект игрока")]
    [SerializeField] private Player _playerHealht;

    [Header("Растояние на котором агртится и отстает враг")]
    [SerializeField] private float _stoppingDistance;

    [Header("Урон врага")]
    [SerializeField] private float _damage;

    private Transform _target;

    bool moovingRight = false;

    bool chill = false;

    bool angry = false;

    bool goBack = false;

    public Transform Target
    {
        get 
        {
            return _target;
        }

        set
        {
            _target = value;
        }
    }

    protected override void Start()
    {
        base.Start();

        _player = GameObject.FindObjectOfType<Player>().transform;

        _healthBar.SetHealthValue(_currentHealth, MaxHealth);

        collider2D = GetComponent<Collider2D>();
    }

    protected void Update()
    {
        if (Vector2.Distance(transform.position, _point.position) < _positionOfPatrol && angry == false)
        {
            chill = true;
        }

        if (Vector2.Distance(transform.position, _player.position) < _stoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }

        if (Vector2.Distance(transform.position, _player.position) > _stoppingDistance)
        {
            goBack = true;
            angry = false;
        }

        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goBack == true)
        {
            GoBack();
        }

        if (_playerHealht.collider2D.GetComponent<Collider2D>().enabled == false)
        {
            _speed = 0;

            enemyAnimator.SetTrigger("die");
        }
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
            enemyAnimator.SetTrigger("die");

            collider2D.GetComponent<Collider2D>().enabled = false;

            if (collider2D.GetComponent<Collider2D>().enabled == false)
            {
                _speed = 0;
            }

            StartCoroutine(Die());
        }
    }

    public void Chill()
    {
        if (transform.position.x > _point.position.x + _positionOfPatrol)
        {
            moovingRight = false;
            _spriteRenderer.flipX = true;

        }
        if (transform.position.x < _point.position.x - _positionOfPatrol)
        {
            moovingRight = true;
            _spriteRenderer.flipX = false;
        }

        if (moovingRight)
        {

            transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
        }
        else
        {

            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
        }
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }

    public void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);
    }

    public void DamagePlayer()
    {
        _playerHealht.GetDamage(_damage);
    }
}
