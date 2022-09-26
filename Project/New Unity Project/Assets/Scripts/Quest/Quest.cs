using Assets.Scripts.Inventory;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets.Scripts.Inventory.Abstract;
using Unity.VisualScripting;
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
<<<<<<< Updated upstream
    [SerializeField] private GameObject go;
    private UIInventory inv;
    private int objectiveAmount;
    private string type;

    void Awake()
    {
        inv = go.GetComponent<UIInventory>();
        inv.inventory.OnInventoryChanged += OnInventoryChanged;
    }
    public void UpdateItemCount(IInventoryItem item)
    {
        var info = item.info.id;
        var inv = new Inventory(12);
        inv.OnInventoryChanged += OnInventoryChanged;
    }

    private void OnInventoryChanged()
    {
        if (objectiveAmount == inv.inventory.GetAllItems(type).Length)
        {
            QuestEnd();
        }
    }

    void QuestEnd()
    {
        var slots = inv.inventory.GetAllSlots();
=======
    public void UpdateItemCount(IInventoryItem item)
    {
        item.info.
>>>>>>> Stashed changes
    }
}


