using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : AbstractEnemy
{
    [Header("������� �� �������� �������")]
    [SerializeField] private float _secondDestroyObject;

    [Header("�������� �����")]
    [SerializeField] private float _speed;

    [Header("����� �������������� �����")]
    [SerializeField] private Transform _point;

    [Header("��������� �� ������� ���� ����� ���������� �� ����� ��������������")]
    [SerializeField] private int _positionOfPatrol;

    [Header("Sprite �����")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("������ ������")]
    [SerializeField] private Transform _player;

    [Header("������ ������")]
    [SerializeField] private Player _playerHealht;

    [Header("��������� �� ������� �������� � ������� ����")]
    [SerializeField] private float _stoppingDistance;

    [Header("���� �����")]
    [SerializeField] private float _damage;

    [Header("������ ��� ���������� HealsBar")]
    [SerializeField] private GameObject _healsBar;

    [SerializeField] private ParticleSystem _particleSystem;

    public GameObject _bones;

    public AudioSource audiosource;

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

        audiosource = GetComponent<AudioSource>();
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

            _healsBar.SetActive(false);
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
        _particleSystem.Play();

        if (_currentHealth <= 0)
        {
            enemyAnimator.SetTrigger("die");

            _healsBar.SetActive(false);

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
        audiosource.Play();
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
