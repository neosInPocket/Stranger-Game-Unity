using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questParent;
    [SerializeField] private Text questDescription;

    private Quest selectedQuest;

    private static QuestLog instanse;

    public static QuestLog Instance
    {
        get 
        {
            if (instanse == null)
            {
                instanse = GameObject.FindObjectOfType<QuestLog>();
            }
            return instanse; 
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AcceptQuest(Quest quest)
    {
        GameObject go = Instantiate(questPrefab, questParent);

        QuestScript questScript = go.GetComponent<QuestScript>();

        quest.MyQuestScript = questScript;

        questScript.MyQuest = quest;

        go.GetComponent<Text>().text = quest.MyTitle;
    }

    public void ShowQuestDescription(Quest quest)
    {
        if (selectedQuest != null)
        {
            selectedQuest.MyQuestScript.DeSelect();
        }

        string objectives = string.Empty;

        selectedQuest = quest;

        string title = quest.MyTitle;

        foreach (Objective obj in quest.MyCollectObjectives)
        {
            objectives += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }

        questDescription.text = string.Format("{0}\n\n<size=18>{1}</size>\n\nײוכü\n<size=18>{2}</size>", title, quest.MyDiscription, objectives);
    }
}
