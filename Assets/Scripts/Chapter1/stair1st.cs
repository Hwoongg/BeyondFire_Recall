using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 챕터1의 1층 계단에 사용되는 스크립트 입니다.
// 하층 이동 시 화재 상태에 따라 이동 여부를 결정합니다.
//

public class stair1st : DuplexStairInteraction {

    // 챕터 1 스테이지 매니저
    // ...

    public Dialogue FireDialogue;
    private Chapter1Manager c1manager;

	// Use this for initialization
	new void Start ()
    {
        base.Start();
        c1manager = FindObjectOfType<Chapter1Manager>();
	}

    public override void downAction()
    {
        // 스테이지 매니저에서 소강상태인지 체크
        if (!c1manager.FireLull)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(FireDialogue);
            return;
        }
        //yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        base.downAction();
    }

    
}
