using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상대좌표 -0.64, -0.03;
public class Ch3_Father : InteractionSystem
{
    GameObject player;
    Animator playerAnimator;

    public GameObject objFather;
    public AnimatorOverrideController fatherAnimator;

    public Dialogue dialogue;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
    }

    public override void doAction()
    {
        StartCoroutine(Routine());
    }

    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }

    IEnumerator Routine()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);

        playerAnimator.runtimeAnimatorController = fatherAnimator;
        player.GetComponent<CharacterMover>().beforeState = CharacterMover.CharState.RESCUE;
        player.GetComponent<CharacterMover>().charState = CharacterMover.CharState.RESCUE;

        // 아버지 오브젝트 캐릭터에 자식으로 추가
        objFather.SetActive(true);

        FindObjectOfType<O2Gauge>().dropIdleParam *= 2;

        gameObject.SetActive(false);
        yield break;
    }
}
