using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Input = UnityEngine.Input;
using UnityEngine.UI;
using TMP_Text = TMPro.TMP_Text;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private AudioSource introSource;
    [SerializeField] private GameObject background;
    [SerializeField] private TMP_Text creditsText;
    
    private IEnumerator Blind()
    {
        GetComponent<AudioSource>().Play();
        CinemachineShake.instance.ShakeCamera(15f, 7f);
        while (globalLight.intensity < 100)
        {
            globalLight.intensity++;
            yield return new WaitForSeconds(0.05f);
        }
        GetComponent<AudioSource>().Stop();
        background.SetActive(true);

        introSource.Play();
        StartCoroutine(ShowCredits());
    }

    private IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(3f);

        introSource.Play();
        creditsText.text = "игра создана с участием \n Санька \n Влады \n Богдана";

        yield return new WaitForSeconds(3f);
        introSource.Play();
        creditsText.text = null;
    }

    public void StartCredits()
    {
        StartCoroutine(Blind());
    }
}
