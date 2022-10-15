using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Kill Quest", menuName = "Quest/Kill Quest")]
public class QuestKill : QuestBase
{
    public Action onInit;

    [System.Serializable]
    public class Objectives
    {
        public EnemyProfile requiredEnemy;

        public int requiredAmount;
    }

    public Objectives[] objectives;

    public override void InitializeQuest()
    {
        RequiredAmount = new int[objectives.Length];

        for (int i = 0; i < objectives.Length; i++)
        {
            RequiredAmount[i] = objectives[i].requiredAmount;
        }

        onInit?.Invoke();

        GameManager.instance.onEnemyDeathCollBack += EnemyDeath;

        QuestManager.instance.player.questKill = this;

        base.InitializeQuest();
    }

    private void EnemyDeath(EnemyProfile slainEnemy)
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if (slainEnemy == objectives[i].requiredEnemy)
            {
                CurrentAmount[i]++;
            }
        }

        if (onQuestComplite != null)
        {
            CheckAmount();
        }
        
    }
}
