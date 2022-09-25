using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Select()
    {
        GetComponent<Text>().color = Color.blue;

        QuestLog.Instance.ShowQuestDescription(MyQuest);
    }

    public void DeSelect()
    {
        GetComponent<Text>().color = Color.black;
    }
}
