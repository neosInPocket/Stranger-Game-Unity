using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButtons : MonoBehaviour
{
    public void QuestAccept()
    {
        QuestManager.instance.CurrentQuestDialogueTrigger.hasActiveQuest = false;

        QuestManager.instance.CurrentQuest.InitializeQuest();

        QuestManager.instance.questUI.SetActive(false);
    }

    public void QuestDecline()
    {
        QuestManager.instance.questUI.SetActive(false);
    }
}
