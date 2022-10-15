using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase : ScriptableObject
{
    public string questName;

    public Action onQuestComplite;

    [TextArea(5, 10)] public string questDiscription;

    public int[] CurrentAmount { get; set; }
    public int[] RequiredAmount { get; set; }

    public bool IsCompleted { get; set; }

    public virtual void InitializeQuest() 
    {
        CurrentAmount = new int[RequiredAmount.Length];
    }
    
    public void CheckAmount()
    {
        for (int i = 0; i < RequiredAmount.Length; i++)
        {
            if (CurrentAmount[i] < RequiredAmount[i])
            {
                return;
            }

            Debug.Log("ÊÂÝÑÒ ÂÛÏÎËÍÅÍ !!! ");

            onQuestComplite?.Invoke();
        }
    }

}
