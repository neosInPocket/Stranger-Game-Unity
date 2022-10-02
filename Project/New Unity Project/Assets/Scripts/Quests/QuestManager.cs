using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject questUI;
    public Text questName;
    public Text questDescription;

    public void SetQuestUI(QuestBase newQuest)
    {
        questUI.SetActive(true);

        questName.text = newQuest.questName;

        questDescription.text = newQuest.questDiscription;
    }
}
