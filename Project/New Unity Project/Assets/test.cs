using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class test : MonoBehaviour
{
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    public bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }

    void Update()
    {
        var result = DoubleClick();
        if (result)
        {
            Debug.Log(true);
        }

    }
}
