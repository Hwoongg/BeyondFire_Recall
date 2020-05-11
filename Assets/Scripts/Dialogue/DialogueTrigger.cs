using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 대화 시작 트리거 객체 컴포넌트 입니다
//

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        // 대화 매니저가 싱글턴임을 상정한 코드
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
