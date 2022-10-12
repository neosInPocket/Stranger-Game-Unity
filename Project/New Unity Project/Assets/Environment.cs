using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private bool isInteracted;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isInteracted || !collider.GetComponent<Player>())
        {
            return;
        }
        GetComponent<Animator>().enabled = true;
        Destroy(this.gameObject, 5f);

        var randomDirection = new Vector2(5f * Random.Range(-0.5f, 0.5f), 5f);
        GetComponent<Rigidbody2D>().velocity = randomDirection;

        if (randomDirection.x < 0)
        {
            var xScale = transform.localScale.x;
            transform.localScale = new Vector2(xScale * -1, transform.localScale.y);
        }
        isInteracted = true;
    }
}
