using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC1_Task : MonoBehaviour
{
    public bool endDialog;

    [SerializeField] private GameObject _dialog;
    [SerializeField] private GameObject _dialog1Finish;
    [SerializeField] private QuestEvent _questEvent;

    public bool finDialog;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (endDialog == true)
        {
            Time.timeScale = 1;

            _questEvent.quest1 = true;

            _dialog.SetActive(false);
        }
        if (finDialog == true)
        {
            Time.timeScale = 1;

            _questEvent.quest1 = false;

            _dialog.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Time.timeScale = 0;

            if (_questEvent.endQuest1 == false)
            {
                _dialog.SetActive(true);
            }
            else
            {
                _dialog1Finish.SetActive(true);
            }
        }
    }
}
