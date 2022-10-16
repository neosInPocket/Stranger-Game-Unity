using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemy;

    public Transform[] spawnPoint;

    private int _rand;

    private int _randPosition;

    [SerializeField] private float _startTimeBtwSpawns;

    private float _timeBtwSpawns;

    void Start()
    {
        _timeBtwSpawns = _startTimeBtwSpawns;
    }

    
    void Update()
    {
        if (_timeBtwSpawns <= 0)
        {
            _rand = Random.Range(0, enemy.Length);

            _randPosition = Random.Range(0, spawnPoint.Length);

            Instantiate(enemy[_rand], spawnPoint[_randPosition].transform.position, Quaternion.identity);

            _timeBtwSpawns = _startTimeBtwSpawns;
        }
        else
        {
            _timeBtwSpawns -= Time.deltaTime;
        }
    }
}
