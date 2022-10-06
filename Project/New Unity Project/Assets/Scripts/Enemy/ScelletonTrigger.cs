using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScelletonTrigger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _speed;

    [SerializeField] private int _positionOfPatrol;

    [SerializeField] private Transform _point; //точка возвращения врага

    [SerializeField] private Transform _player;

    [SerializeField] private float _stoppingDistance;

    bool moovingRight = false;

    bool chill = false;

    bool angry = false;

    bool goBack = false;

    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>().transform;
    }

    void Update()
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

            _speed = 4;
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
        else if(goBack == true)
        {
            GoBack();
        }
    }

    void Chill()
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

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);
    }
}
