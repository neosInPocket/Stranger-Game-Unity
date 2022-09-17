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

    // Start is called before the first frame update
    void Start()
    {
        healthFill = 1f;

        manaFill = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        healthFill =  player.Health / player.MaxHealth;

        healthBar.fillAmount = healthFill;
    }
}
