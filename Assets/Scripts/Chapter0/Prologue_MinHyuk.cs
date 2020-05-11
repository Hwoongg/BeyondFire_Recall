using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue_MinHyuk : InteractionSystem
{
    public Dialogue JH1;
    public Dialogue MH1;
    public Dialogue JH2;
    public Dialogue MH2;
    public Dialogue JH3;
    public Dialogue MH3;
    public Dialogue JH4;
    public Dialogue MH4;
    public Dialogue JH5;
    public Dialogue MH5;
    public Dialogue JH6;



    public override void doAction()
    {
        StartCoroutine("TalkRoutine");
    }

    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }

    IEnumerator TalkRoutine()
    {
        DialogueManager.Instance().StartDialogue(JH1);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        DialogueManager.Instance().StartDialogue(MH1);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        DialogueManager.Instance().StartDialogue(JH2);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        DialogueManager.Instance().StartDialogue(MH2);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        DialogueManager.Instance().StartDialogue(JH3);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        DialogueManager.Instance().StartDialogue(MH3);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        DialogueManager.Instance().StartDialogue(JH4);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        DialogueManager.Instance().StartDialogue(MH4);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        DialogueManager.Instance().StartDialogue(JH5);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        DialogueManager.Instance().StartDialogue(MH5);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        DialogueManager.Instance().StartDialogue(JH6);
        yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

        yield break;
    }
}
