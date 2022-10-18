using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class FinalSceneHandler : MonoBehaviour
{
    [SerializeField] private GameObject LightEnvironment;
    [SerializeField] private AudioSource environmentSource;
    [SerializeField] private AudioSource bulbSmashSource;
    [SerializeField] private AudioSource flashClickSource;
    [SerializeField] private GameObject labDoorTiles;
    public UnityEvent onTriggerEnter;
    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if (!player)
        {
            return;
        }
        labDoorTiles.SetActive(true);
        onTriggerEnter?.Invoke();
        LightEnvironment.gameObject.SetActive(false);
        bulbSmashSource.Play();
        environmentSource.Play();
        GetComponent<Collider2D>().enabled = false;

        StartCoroutine(EnablePlayerFlash(player));
    }
    private IEnumerator EnablePlayerFlash(Player player)
    {
        yield return new WaitForSeconds(2);
        player.gameObject.GetComponentInChildren<Light2D>().enabled = true;
        flashClickSource.Play();
    }
}
