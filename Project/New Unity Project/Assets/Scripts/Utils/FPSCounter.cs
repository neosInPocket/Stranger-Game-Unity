using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private int avgFrameRate;
    private TMP_Text displayText;

    void Start()
    {
        displayText = GetComponent<TMP_Text>();
    }
    void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        displayText.text = "fps: " + avgFrameRate.ToString();
    }
}
