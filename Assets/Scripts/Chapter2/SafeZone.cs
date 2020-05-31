using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// 구조된 사람들 대피시킬 지점 스크립트 입니다.
//

public class SafeZone : InteractionSystem
{

    private GameObject player;
    private Animator playerAnimator;
    protected CharacterMover playerMover;


    public AnimatorOverrideController IdleAnimator;

    private int SafeCount;
    [SerializeField] string InfoText;

    // Use this for initialization
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        playerMover = player.GetComponent<CharacterMover>();

        SafeCount = 0;
    }

    public override void doAction()
    {
        if (playerMover.charState == CharacterMover.CharState.RESCUE)
        {
            playerAnimator.runtimeAnimatorController = IdleAnimator;
            playerMover.beforeState = CharacterMover.CharState.IDLE;
            playerMover.charState = CharacterMover.CharState.IDLE;
            SafeCount++;
            FindObjectOfType<O2Gauge>().dropIdleParam = 1.0f;
        }
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        FindObjectOfType<PlaceInfoUI>().TextOn(InfoText);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FindObjectOfType<PlaceInfoUI>().TextOff();
    }

}
