﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Chapter 1. 양호선생 구출 이벤트 클래스 스크립트
//
public class eventHelpMinHyuk : InteractionSystem
{
    public Dialogue dialogue;
    public Dialogue overMessage;

    public GameObject player;
    private Animator playerAnimator;

    public AnimatorOverrideController helpMHAnimator;
    
	void Start ()
    {
        playerAnimator = player.GetComponent<Animator>();
    }

    public override void doAction()
    {
        StartCoroutine(HelpMHEvent());
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
        DialogueManager.Instance().StartDialogue(dialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);

        if (player.GetComponent<CharacterMover>().charState != CharacterMover.CharState.RESCUE)
        {
            playerAnimator.runtimeAnimatorController = helpMHAnimator;
            player.GetComponent<CharacterMover>().beforeState = CharacterMover.CharState.RESCUE;
            player.GetComponent<CharacterMover>().charState = CharacterMover.CharState.RESCUE;

            Destroy(gameObject);
        }
        else
        {
            DialogueManager.Instance().StartDialogue(overMessage);
        }
        yield break;
    }
}
