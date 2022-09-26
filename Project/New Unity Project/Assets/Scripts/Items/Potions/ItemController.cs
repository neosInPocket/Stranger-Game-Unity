using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject pickUpPf;
    [SerializeField] private GameObject handler;
    [SerializeField] private GameObject inventory; 
    private GameObject message;

    private bool isInRange;

    void OnTriggerEnter2D()
    {
        message = Instantiate(pickUpPf, transform.GetChild(0).position, Quaternion.identity);
        isInRange = true;
    }

    void OnTriggerExit2D()
    {
        Destroy(message);
        isInRange = false;
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }
}
