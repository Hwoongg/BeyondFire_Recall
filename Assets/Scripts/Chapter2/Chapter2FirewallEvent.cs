using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2FirewallEvent : InteractionSystem
{
    public Dialogue dialogue;

    // 대화 끝나고 생성될 트리거 프리팹
    public GameObject MonsterTrigger;

    [HideInInspector]public bool isReady;

	// Use this for initialization
	void Start ()
    {
        isReady = true;
	}
	

    public override void doAction()
    {
        if(isReady)
            StartCoroutine(FirewallEvent());
    }
    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }

    IEnumerator FirewallEvent()
    {
        // 대화 출력
        DialogueManager.Instance().StartDialogue(dialogue);

        yield return new WaitUntil(() =>
        DialogueManager.Instance().canvasObj.activeSelf == false);

        // 괴물 이벤트 트리거 활성화
        MonsterTrigger.SetActive(true);
        yield break;
    }
}
