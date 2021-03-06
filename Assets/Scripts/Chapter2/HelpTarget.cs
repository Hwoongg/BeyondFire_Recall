﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpTarget : InteractionSystem {

    private GameObject player;
    private Animator playerAnimator;

    public AnimatorOverrideController HelpAnimator;

    public Dialogue deadSentence;
    [SerializeField] Dialogue overMessage;

    private float LifeTime;
    [SerializeField] float timeLimit = 120;
    public enum State
    {
        Normal,
        Limit
    }
    public State state;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            playerAnimator = player.GetComponent<Animator>();
            LifeTime = 0;
        }
    }
    private void Update()
    {
        LifeTime += Time.deltaTime;
    }

    public override void doAction()
    {
        // 체류시간이 120이 넘어가면 구조할 수 없다.
        if(state == State.Limit && LifeTime > timeLimit)
        {
            // 구출 불가 대사 출력
            FindObjectOfType<DialogueManager>().StartDialogue(deadSentence);
            return;
        }

        if(player.GetComponent<CharacterMover>().charState == CharacterMover.CharState.RESCUE)
        {
            DialogueManager.Instance().StartDialogue(overMessage);
            return;
        }

        playerAnimator.runtimeAnimatorController = HelpAnimator;
        player.GetComponent<CharacterMover>().beforeState = CharacterMover.CharState.RESCUE;
        player.GetComponent<CharacterMover>().charState = CharacterMover.CharState.RESCUE;

        // 방독면 있는지 체크
        if (FindObjectOfType<InventorySystem>().CheckActiveItem("GasMask"))
        {
            
            FindObjectOfType<O2Gauge>().dropIdleParam = 1.0f;
            FindObjectOfType<InventorySystem>().
                RemoveItem(ItemSystem.ItemType.ACTIVE, "GasMask");

        }
        else
        {
            FindObjectOfType<O2Gauge>().dropIdleParam = 2.0f;
        }

        player.GetComponent<CharacterMover>().IconStateClear();
        
        gameObject.SetActive(false);
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }
}
