using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject questUI;

    public QuestDialogueTrigger CurrentQuestDialogueTrigger { get; set; }

    public Text questName;

    public Text questDescription;

    public QuestBase CurrentQuest { get; set; }

    public void SetQuestUI(QuestBase newQuest)
    {
        CurrentQuest = newQuest;

        questUI.SetActive(true);

        questName.text = newQuest.questName;

        questDescription.text = newQuest.questDiscription;
    }
}
