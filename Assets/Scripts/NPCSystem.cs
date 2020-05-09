using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 단방향 대화만 있는 기본 NPC 스크립트
//

public class NPCSystem : InteractionSystem
{
    
    public Dialogue dialogue;

    public override void doAction()
    {
        // 대화 매니저가 싱글턴임을 상정한 코드
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
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
