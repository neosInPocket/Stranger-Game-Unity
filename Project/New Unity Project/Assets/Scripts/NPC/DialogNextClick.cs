using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNextClick : MonoBehaviour
{
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _text2;
    [SerializeField] private GameObject _objectQuest;

    private bool isText = true;

    public bool finDialog;

    [SerializeField] private NPC1_Task _npc1TaskScript;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isText == true)
            {
                isText = false;
            }
            else
            {
                if (finDialog == false)
                {
                    isText = true;

                    _objectQuest.SetActive(true);

                    _npc1TaskScript.endDialog = true;
                }
                else
                {
                    isText = true;

                    _npc1TaskScript.finDialog = true;
                }
            }
        }

        if (isText == true)
        {
            _text.SetActive(true);

            _text2.SetActive(false);
        }
        else
        {
            _text.SetActive(false);

            _text2.SetActive(true);
        }
    }
}
