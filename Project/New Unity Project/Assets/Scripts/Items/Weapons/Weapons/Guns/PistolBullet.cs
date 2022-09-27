using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    public float damage;

    public float speed;
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        damage = 25;
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        if (hitInfo.name != "Player")
        {
            Destroy(gameObject);
        }

        Enemy enemy;
        if (hitInfo.transform.gameObject.GetComponent<Enemy>() != null)
        {
            enemy = hitInfo.transform.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        
        
    }
}
