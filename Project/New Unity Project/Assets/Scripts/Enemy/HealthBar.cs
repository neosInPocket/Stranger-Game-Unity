using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Vector3 _offSet;

    private void Update()
    {
        _slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offSet);
    }

    public void SetHealthValue(float currentHealth, float maxHealth)
    {
        _slider.gameObject.SetActive(currentHealth < maxHealth);
        _slider.value = currentHealth;
        _slider.maxValue = maxHealth;
    }
}
