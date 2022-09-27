using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveNPC1 : MonoBehaviour
{
    [SerializeField] private GameObject _nps1;
    
    [SerializeField] private GameObject _nps1_1;
    [SerializeField] private GameObject _sceleton;

    void Update()
    {
        if (_sceleton == false)
        {
            _nps1.SetActive(false);

            _nps1_1.SetActive(true);
        }
    }
}
