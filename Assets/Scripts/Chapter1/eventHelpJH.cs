using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eventHelpJH :InteractionSystem {

    public Dialogue dialogue;

    

    private void Start()
    {
        
    }

    public override void doAction()
    {
        // 대화 매니저가 싱글턴임을 상정한 코드
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        StartCoroutine("HelpJHEvent");
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    IEnumerator HelpJHEvent()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        // 페이드 아웃
        FindObjectOfType<FadeEffect>().FadeOut();

        yield return new WaitForSeconds(5.0f);

        // 다음 씬으로
        SceneManager.LoadScene("Chapter_2_v3");


        yield break;
    }
}
