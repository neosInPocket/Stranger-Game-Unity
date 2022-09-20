using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : HealthPotionAbstract
{
    void Awake()
    {
        healthAmount = 30;
    }
}
