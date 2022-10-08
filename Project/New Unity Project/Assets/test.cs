using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private Player player;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            player.GetDamage(1f);
        }
    }
}
