using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyBehaviour : AbstractEnemy
{
    [Header("EnemyBehaviour")]
    [SerializeField] private Transform _rayCast;
    [SerializeField] private LayerMask _rayCastMask;
    [SerializeField] private float _rayCastLanght;
    [SerializeField] private float _attackDistance;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float _defence = 0;
    [SerializeField] private float _timer;
    //[SerializeField] private GameObject _playerObject;

    private RaycastHit2D _hit;
    private GameObject _target;
    private Animator _animator;
    private float _distance;
    private bool _attackMode;
    private bool _inRange;
    private bool _cooling;
    private float _intTimer;

    void Awake()
    {
        _intTimer = _timer;   

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_inRange)
        {
            _hit = Physics2D.Raycast(_rayCast.position, Vector2.left, _rayCastLanght, _rayCastMask);

            RayCastDebugger();
        }

        if (_hit.collider != null)
        {
            EnemyLogic();
        }
        else if (_hit.collider == null)
        {
            _inRange = false;
        }

        if (_inRange == false)
        {
            _animator.SetBool("canWalk", false);

            StopAttack();
        }
    }

    void EnemyLogic()
    {
        _distance = Vector2.Distance(transform.position, _target.transform.position);

        if (_distance > _attackDistance)
        {
            Move();

            StopAttack();
        }
        else if (_attackDistance >= _distance && _cooling == false)
        {
            Attack();
        }

        if (_cooling)
        {
            _animator.SetBool("Attack", false);
        }
    }

    void StopAttack()
    {
        _cooling = false;

        _attackMode = false;

        _animator.SetBool("Attack", false);
    }

    void Attack()
    {
        _timer = _intTimer;

        _attackMode = true;

        _animator.SetBool("canWalk", false);

        _animator.SetBool("Attack", true);
    }

    void Move()
    {
        _animator.SetBool("canWalk", true);

        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Skell_attack"))
        {
            Vector2 targetPosition = new Vector2(_target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
        }
    }

    void RayCastDebugger()
    {
        if (_distance > _attackDistance)
        {
            Debug.DrawRay(_rayCast.position, Vector2.left * _rayCastLanght, Color.red);
        }
        else if (_attackDistance > _distance)
        {
            Debug.DrawRay(_rayCast.position, Vector2.left * _rayCastLanght, Color.green);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            _target = trigger.gameObject;

            _inRange = true;
        }
    }

    protected override void Start()
    {
        base.Start();
        _healthBar.SetHealthValue(_currentHealth, MaxHealth);
    }

    private IEnumerator Die()
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
