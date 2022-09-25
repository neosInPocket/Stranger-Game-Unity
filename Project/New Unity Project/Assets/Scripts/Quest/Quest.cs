using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest 
{
    [SerializeField] private string title;

    [SerializeField] private string description;

    [SerializeField] private CollectObjective[] collectObjectives; 

    public QuestScript MyQuestScript { get; set; }

    public string MyTitle
    {
        get
        {
            return title;
        }

        set
        {
            title = value;
        }
    }

    public string MyDiscription
    {
        get
        {
            return description;
        }

        set 
        {
            description = value; 
        }
    }

   public CollectObjective[] MyCollectObjectives
    {
        get
        {
            return collectObjectives;
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

[Serializable]
public abstract class Objective
{
    [SerializeField] private int amount;
    [SerializeField] private string type;

    private int currentAmount;

    public int MyAmount
    {
        get 
        {
            return amount;
        }
    }

    public int MyCurrentAmount
    {
        get 
        {
            return currentAmount; 
        }

        set 
        {
            currentAmount = value; 
        }
    }

    public string MyType
    {
        get
        {
            return type;
        }
    }
}

[Serializable]
public class CollectObjective : Objective
{

}
