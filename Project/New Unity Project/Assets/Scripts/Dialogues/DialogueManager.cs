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

    public bool inDialogue;

    public float delay = 0.001f;

    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();


    private DialogueBase currentDialogue;

    private bool isCurrentlyTyping;

    private string completeText;

    public void EnqueueDialogue(DialogueBase dialogueBase)
    {
        dialogueBox.SetActive(true);

        dialogueInfo.Clear();

        currentDialogue = dialogueBase;

        foreach (DialogueBase.Info info in dialogueBase.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        DequeueDialogue();
    }


    public void DequeueDialogue()
    {
        if (isCurrentlyTyping == true)
        {
            CompleteText();

            StopAllCoroutines();

            isCurrentlyTyping = false;

            return;
        }

        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();

            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();

        completeText = info.myText;

        diallogueName.text = info.myName;

        diallogueText.text = info.myText;

        dialoguePortrait.sprite = info.portrait;

        characterName.text = info.myCharacterName;

        diallogueText.text = "";

        StartCoroutine(TypeText(info));
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        isCurrentlyTyping = true;

        foreach (char item in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);

            diallogueText.text += item;
        }

        isCurrentlyTyping = false;
    }

    private void CompleteText()
    {
        diallogueText.text = completeText;
    }

    public void EndOfDialogue()
    {
        dialogueBox.SetActive(false);

        CheckIfDialogueQuest();
    }

    private void CheckIfDialogueQuest()
    {
        if (currentDialogue is DialogueQuest)
        {
            DialogueQuest dialogueQuest = currentDialogue as DialogueQuest;

            QuestManager.instance.SetQuestUI(dialogueQuest.quest);
        }
    }
}
