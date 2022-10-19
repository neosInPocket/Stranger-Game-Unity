using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CometsSpawn : MonoBehaviour
{
    //private Player player;
    [SerializeField] private GameObject redLine;
    [SerializeField] private GameObject cometObject;
    [SerializeField] private Player player;
    private AudioSource beepSource;
    public bool isOver;
    IEnumerator SpawnStart()
    {
        var redLineObject = Instantiate(redLine.gameObject, player.transform.position, Quaternion.identity);
        redLineObject.SetActive(false);
        beepSource = redLineObject.GetComponent<AudioSource>();

        while (!isOver)
        {
            yield return new WaitForSeconds(10f);
            
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.25f);

                redLineObject.transform.position = player.transform.position;
                redLineObject.SetActive(true);
                beepSource.PlayOneShot(beepSource.clip);

                yield return new WaitForSeconds(0.15f);
                redLineObject.SetActive(false);
            }

            yield return new WaitForSeconds(1f);

            var comet = Instantiate(cometObject.gameObject, redLineObject.transform.position, Quaternion.identity);
            CinemachineShake.instance.ShakeCamera(10f, 1f);
            Destroy(comet, 3f);
        }
    }

    public void StartComets()
    {
        StartCoroutine(SpawnStart());
    }

    public void TurnOnOff(bool action)
    {
        isOver = action;
    }
}
