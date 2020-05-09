using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventToiletStudent : InteractionSystem
{
    public Dialogue dialogue;

    public override void doAction()
    {
        StartCoroutine("ToiletStudentEvent");
        
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    IEnumerator ToiletStudentEvent()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        DestroyObject(gameObject);
        yield break;
    }
}
