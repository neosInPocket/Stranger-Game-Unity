using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PistolBullet : MonoBehaviour
{
    private float _damage;
    [SerializeField] private GameObject _explosionEffect;
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
            var instance = Instantiate(_explosionEffect, transform.position - new Vector3(0.01f, 0.01f), transform.rotation);
            Destroy(instance, .2f);
        }
        
        AbstractEnemy enemy;
        if (hitInfo.transform.gameObject.GetComponent<AbstractEnemy>() != null)
        {
            enemy = hitInfo.transform.gameObject.GetComponent<AbstractEnemy>();
            enemy.TakeDamage(Damage);
        }
    }
}
