using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForAttack : MonoBehaviour
{
    [Header("Sprite врага")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Collider2D устонавливается с зади врага")]
    [SerializeField] private BoxCollider2D _collider2D;

    [Header("Collider2D врага")]
    [SerializeField] private BoxCollider2D _enemyCollider2D;

    private float newX;

    private float newY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            if (_spriteRenderer.flipX == false)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
        }
    }

    void Update()
    {
        if (_spriteRenderer.flipX == true)
        {
            newX = 1.1f;

            newY = 0f;

            _collider2D.GetComponent<BoxCollider2D>().offset = new Vector2(newX, newY);
        }
        else
        {
            newX = -1.1f;

            newY = 0f;

            _collider2D.GetComponent<BoxCollider2D>().offset = new Vector2(newX, newY);
        }

        if (_enemyCollider2D.GetComponent<Collider2D>().enabled == false)
        {
            _collider2D.GetComponent<Collider2D>().enabled = false;
        }
    }
}
