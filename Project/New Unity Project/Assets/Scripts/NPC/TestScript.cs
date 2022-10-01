using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public QuestBase quest;

    private void Start()
    {
        quest.InitializeQuest();
    }
}
