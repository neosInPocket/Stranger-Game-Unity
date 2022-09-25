using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Quest[] quests;
    [SerializeField] private QuestLog questLog;

    private void Awake()
    {
        questLog.AcceptQuest(quests[0]);
        //questLog.AcceptQuest(quests[1]);
    }
}
