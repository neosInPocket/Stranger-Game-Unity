using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScelletonTrigger : MonoBehaviour
{
    public float speed;

    public int positionOfPatrol;
    public Transform point;  //точка возвращения врага

    bool moovingRight = false;

    Transform player;
    public float stoppingDistance;

    bool chill = false;
    bool angry = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol)
        {
            Chill();
        }

        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            Angry();
        }

        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
    }

    void Chill()
    {
        //if (transform.position.x > point.position.x + positionOfPatrol)
        //{
        //    moovingRight = false;
        //}
        if (transform.position.x < point.position.x - positionOfPatrol)
        {
            moovingRight = true;
        }

        //if (moovingRight)
        //{
        //    transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        //}
        if (moovingRight == false)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
