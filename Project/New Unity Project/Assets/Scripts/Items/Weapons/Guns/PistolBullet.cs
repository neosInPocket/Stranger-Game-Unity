using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PistolBullet : MonoBehaviour
{
    private float _damage;
    public float Damage 
    {
        get
        { 
            return _damage;
        }

        set
        {
            _damage = value;
        }
    }

    public float speed;
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.isTrigger && !hitInfo.gameObject.GetComponentInParent<Player>() && !hitInfo.gameObject.GetComponent<TilemapCollider2D>())
        {
            Destroy(this.gameObject);
        }
        
        Enemy enemy;
        if (hitInfo.transform.gameObject.GetComponent<Enemy>() != null)
        {
            enemy = hitInfo.transform.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Damage);
        }

        Debug.Log(hitInfo.name);
    }
}
