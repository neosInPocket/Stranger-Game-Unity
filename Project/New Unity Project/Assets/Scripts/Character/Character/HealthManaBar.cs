using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManaBar : MonoBehaviour
{
    public Image healthBar;

    public float healthFill;

    public Player player;

    [SerializeField] private Image defenceBarImage;
    
    void Start()
    {
        healthFill = 1f;
    }
    void Update()
    {
        healthFill = player.health / player.maxHealth;

        healthBar.fillAmount = healthFill;

        defenceBarImage.fillAmount = player.defence / 9f;
    }
}
