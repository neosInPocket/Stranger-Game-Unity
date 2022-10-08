using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Текст для сообщения над врогом")]
    [SerializeField] private Image _imageText;

    [Header("Полоса здоровья")]
    [SerializeField] private Slider _slider;

    [SerializeField] private Vector3 _offSet;

    [SerializeField] private Vector3 _offSetImageText;

    private void Update()
    {
        _slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offSet);

        _imageText.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offSetImageText);
    }

    public void SetHealthValue(float currentHealth, float maxHealth)
    {
        _slider.gameObject.SetActive(currentHealth < maxHealth);

        _slider.value = currentHealth;

        _slider.maxValue = maxHealth;
    }
}
