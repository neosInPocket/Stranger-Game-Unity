using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractHandler : MonoBehaviour
{
    [SerializeField] private GameObject pickUpPref;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<ChestBox>())
        {
            pickUpPref.gameObject.SetActive(true);
            pickUpPref.gameObject.GetComponentInChildren<TMP_Text>().text = "Open";
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<ChestBox>())
        {
            pickUpPref.gameObject.SetActive(false);
            pickUpPref.gameObject.GetComponentInChildren<TMP_Text>().text = "Pick up";
        }
    }
}
