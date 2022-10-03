using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExiteButton : MonoBehaviour
{
    public GameObject dialoguePanel;

    public void ExiteDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
