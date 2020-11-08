using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_JooIm : InteractionSystem
{
    public Dialogue JH1;
    public Dialogue JooIm1;
    public Dialogue JH2;
    public Dialogue JooIm2;
    public Dialogue JH3;
    public Dialogue JooIm3;
    public Dialogue JH4;
    public Dialogue JooIm4;
    public Dialogue JH5;
    public Dialogue Message;

    [HideInInspector]public bool isReqOver = false;
    public Dialogue reqOverDlg;
    public override void doAction()
    {
        // 대화 매니저가 싱글턴임을 상정한 코드
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        // 코루틴으로 시작
        StartCoroutine(JooImTalk());
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    IEnumerator JooImTalk()
    {
        if (isReqOver)
        {
            DialogueManager.Instance().StartDialogue(reqOverDlg);
        }
        else
        {
            DialogueManager.Instance().StartDialogue(JH1);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

            DialogueManager.Instance().StartDialogue(JooIm1);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

            DialogueManager.Instance().StartDialogue(JH2);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

            DialogueManager.Instance().StartDialogue(JooIm2);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

            DialogueManager.Instance().StartDialogue(JH3);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

            DialogueManager.Instance().StartDialogue(JooIm3);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

            DialogueManager.Instance().StartDialogue(JH4);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

            DialogueManager.Instance().StartDialogue(JooIm4);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

            DialogueManager.Instance().StartDialogue(JH5);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);

            Note n = FindObjectOfType<Note>();
            if (n != null)
            {
                n.RemoveMission("GoToRest");
                var inven = FindObjectOfType<InventorySystem>();

                n.AddMission("GoToMH");
                // 이미 열쇠를 갖고있다면
                if (inven.CheckPassiveItem("Key"))
                {
                    // 곧장 창고로 이동하는 퀘스트
                    n.AddMission("GoToStore");
                }
                else
                {
                    n.AddMission("FindKey");
                }
                
                isReqOver = true;
            }
            DialogueManager.Instance().textTalker.color = new Color(0.2f, 0.6f, 1);
            DialogueManager.Instance().textSentence.color = new Color(0.2f, 0.6f, 1);
            DialogueManager.Instance().textSentence.alignment = TextAnchor.UpperCenter;
            DialogueManager.Instance().StartDialogue(Message);
            yield return new WaitUntil(() => DialogueManager.Instance().canvasObj.activeSelf == false);
            DialogueManager.Instance().textTalker.color = new Color(1, 1, 1);
            DialogueManager.Instance().textSentence.color = new Color(1, 1, 1);
            DialogueManager.Instance().textSentence.alignment = TextAnchor.UpperLeft;
        }
        
        yield break;
    }
}
