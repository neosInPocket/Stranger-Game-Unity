using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AbstractEnemy
{
    [SerializeField] private GameObject _drop;
    [SerializeField] private float defence;
    private bool _isHitting = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().GetDamage(1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 8f, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.GetComponent<AbstractEnemy>() != null)
        {
            TakeDamage(1);
        }
    }

    private IEnumerator Die()
    {
        if (_drop != null)
        {
            Instantiate(_drop, transform.position, Quaternion.identity);
        }
        _isHitting = true;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        _currentHealth -= damage - defence;

        if (_currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }
}
