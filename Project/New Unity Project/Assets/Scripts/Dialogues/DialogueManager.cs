using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Нужно пофиксить!" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }

    public GameObject dialogueBox;

    public Text characterName;

    public Text diallogueName;
    
    public Text diallogueText;

    public Image dialoguePortrait;

    public float delay = 0.001f;

    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();


    public void EnqueueDialogue(DialogueBase dialogueBase)
    {
        dialogueBox.SetActive(true);

        dialogueInfo.Clear();

        foreach (DialogueBase.Info info in dialogueBase.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        DequeueDialogue();
    }


    public void DequeueDialogue()
    {
        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();

            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();

        diallogueName.text = info.myName;

        diallogueText.text = info.myText;

        dialoguePortrait.sprite = info.portrait;

        characterName.text = info.myCharacterName;

        StartCoroutine(TypeText(info));
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        diallogueText.text = "";

        foreach (char item in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            diallogueText.text += item;
            yield return null;
        }
    }

    public void EndOfDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
