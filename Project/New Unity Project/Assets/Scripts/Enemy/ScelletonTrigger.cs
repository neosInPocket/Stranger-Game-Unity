using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScelletonTrigger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public float speed;

    public int positionOfPatrol;
    public Transform point;  //точка возвращения врага

    bool moovingRight = false;

    Transform player;
    public float stoppingDistance;

    bool chill = false;
    bool angry = false;
    bool goBack = false;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            chill = true;
        }

        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;

            speed = 4;
        }

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
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
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            moovingRight = false;
            spriteRenderer.flipX = true;

        }
        if (transform.position.x < point.position.x - positionOfPatrol)
        {
            moovingRight = true;
            spriteRenderer.flipX = false;
        }

        if (moovingRight)
        {
            
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }
}
