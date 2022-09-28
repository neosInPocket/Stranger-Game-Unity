using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent : MonoBehaviour
{
    public bool quest1;

    public bool endQuest1;

    [SerializeField] private GameObject _windowQuest;


    void Start()
    {
        
    }

    void Update()
    {
        if (endQuest1 == false)
        {
            if (quest1 == true)
            {
                _windowQuest.SetActive(true);
            }
            else
            {
                _windowQuest.SetActive(false);
            }
        }
        else
        {
            _windowQuest.SetActive(false);
        }
    }
}
