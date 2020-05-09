using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Chapter 1. 양호선생 구출 이벤트 클래스 스크립트
//
public class eventHelpMinHyuk : InteractionSystem
{
    public Dialogue dialogue;

    public GameObject player;
    private Animator playerAnimator;

    public AnimatorOverrideController helpMHAnimator;

	// Use this for initialization
	void Start () {
        playerAnimator = player.GetComponent<Animator>();
    }

    public override void doAction()
    {
        StartCoroutine("HelpMHEvent");
    }
    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }
    IEnumerator HelpMHEvent()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        playerAnimator.runtimeAnimatorController = helpMHAnimator;

        Destroy(gameObject);
        yield break;
    }
}
