using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroDistanceEnemy : MonoBehaviour
{
    [Header("�������� �����")]
    [SerializeField] private Animator enemyAnimator;

    private Enemy _parent;

    private void Start()
    {
        _parent = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            enemyAnimator.SetBool("isAttack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            enemyAnimator.SetBool("isAttack", false);
        }
    }
}
