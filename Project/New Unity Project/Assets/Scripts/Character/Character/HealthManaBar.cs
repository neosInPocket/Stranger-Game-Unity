using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManaBar : MonoBehaviour
{
    public Image healthBar;

    public Image manaBar;

    public float healthFill;

    public float manaFill;

    public Player player;

    [SerializeField] private Image defenceBarImage;
    
    void Start()
    {
        healthFill = 1f;

        manaFill = 1f;
    }
    void Update()
    {
        healthFill = player.health / player.maxHealth;

        healthBar.fillAmount = healthFill;

        defenceBarImage.fillAmount = player.defence / 8f;
        Debug.Log(player.defence);
    }
}
