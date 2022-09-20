using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class HealthPotionAbstract : MonoBehaviour
{
    [SerializeField] protected float healthAmount;
    public event Action<object> OnTriggerEnter;

    void OnTriggerEnter2D()
    {
        Debug.Log("Entered");
        OnTriggerEnter?.Invoke(this);
    }
}
