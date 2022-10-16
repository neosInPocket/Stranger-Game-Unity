using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractHandler : MonoBehaviour
{
    [SerializeField] private GameObject pickUpPref;
    private ChestBox _activeChest;
    void OnTriggerEnter2D(Collider2D collider)
    {
        var chest = collider.GetComponent<ChestBox>();

        if (chest && chest.isOpened)
        {
            return;
        } 
        
        if (chest)
        {
            pickUpPref.gameObject.SetActive(true);
            pickUpPref.gameObject.GetComponentInChildren<TMP_Text>().text = "Open";
            _activeChest = chest;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<ChestBox>())
        {
            pickUpPref.gameObject.SetActive(false);
            pickUpPref.gameObject.GetComponentInChildren<TMP_Text>().text = "Pick up";
            _activeChest = null;
        }
    }

    void Update()
    {
        if (_activeChest && !_activeChest.isOpened && Input.GetKeyDown(KeyCode.E))
        {
            _activeChest.Open();
            _activeChest = null;
            pickUpPref.gameObject.SetActive(false);
            pickUpPref.gameObject.GetComponentInChildren<TMP_Text>().text = "Pick up";
        }
    }
}
