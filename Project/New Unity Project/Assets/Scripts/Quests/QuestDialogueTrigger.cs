using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDialogueTrigger : DialogueTrigger
{
    [Header("Quest Dialogue Info")]
    public bool hasActiveQuest;

    public DialogueQuest[] dialogueQuests;

    public int QuestIndex { get; set; }

    public override void Interact()
    {
        if (hasActiveQuest)
        {
            DialogueManager.instance.EnqueueDialogue(dialogueQuests[QuestIndex]);

            QuestManager.instance.CurrentQuestDialogueTrigger = this;
        }
        else
        {
            base.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(collision.name);

            Interact();
        }
    }
}
