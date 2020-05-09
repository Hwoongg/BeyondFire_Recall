using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enventValve : InteractionSystem {

    // 스프링 클러 오브젝트들
    public GameObject sprinklerGroup;
    private GameObject[] sprinklerList;

    public Dialogue dialogue;

    // Use this for initialization

    private void Awake()
    {
        sprinklerList = new GameObject[5];
        for (int i = 0; i < sprinklerGroup.transform.childCount; i++)
            sprinklerList[i] = sprinklerGroup.transform.GetChild(i).GetChild(0).gameObject;

    }
    void Start () {
		
	}

    public override void doAction()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        OnSprinklerAll();
    }
    public override void upAction()
    {
        doAction();
    }
    public override void downAction()
    {
        doAction();
    }
    void OnSprinklerAll()
    {
        // 스프링클러 파티클 모두 활성화
        for (int i = 0; i < sprinklerList.Length; i++)
            sprinklerList[i].SetActive(true);

        // 매니저에서 화재 진압상태로 전환
        FindObjectOfType<Chapter1Manager>().StartFireLull();

    }
}
