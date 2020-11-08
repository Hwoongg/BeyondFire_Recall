using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventToiletStudent : InteractionSystem
{
    public Dialogue dialogue;
    public Dialogue dialogue2;

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
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue2);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);

        FindObjectOfType<Note>().CountUp("Resque");

        DestroyObject(gameObject);
        yield break;
    }
}
