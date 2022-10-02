using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Basic Dialogues Info")]
    public DialogueBase[] dialogueBase;

    [HideInInspector] public int index;

    private bool nextDialogueOnInteract;

    public virtual void Interact()
    {
        if (nextDialogueOnInteract && !DialogueManager.instance.inDialogue)
        {
            DialogueManager.instance.EnqueueDialogue(dialogueBase[index]);

            if (index < dialogueBase.Length - 1)
            {
                index++;
            }
        }
        else
        {
            DialogueManager.instance.EnqueueDialogue(dialogueBase[index]);
        }
    }
}
