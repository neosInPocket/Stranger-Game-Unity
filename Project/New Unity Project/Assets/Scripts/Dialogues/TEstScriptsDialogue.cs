using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEstScriptsDialogue : MonoBehaviour
{
    public DialogueBase dialogue;

    public void TriggerDialogue()
    {
        DialogueManager.instance.EnqueueDialogue(dialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerDialogue();
        }


    }
}
