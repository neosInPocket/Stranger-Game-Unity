using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moovingSpeed;


    private void Awake()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindObjectOfType<Player>().transform;
        }

        transform.position = new Vector3() { x = playerTransform.position.x, y = playerTransform.position.y, z = playerTransform.position.y - 10 };
    }

    private void Update()
    {
        if (playerTransform)
        {
            Vector3 target = new Vector3() { x = playerTransform.position.x, y = playerTransform.position.y, z = playerTransform.position.y - 10 };

            Vector3 pos = Vector3.Lerp(transform.position, target, moovingSpeed * Time.deltaTime);

            transform.position = pos;
        }

        
    }

}
