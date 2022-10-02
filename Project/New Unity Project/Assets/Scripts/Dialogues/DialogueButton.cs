using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    public void GetNextLibe()
    {
        DialogueManager.instance.DequeueDialogue();
    }
}
