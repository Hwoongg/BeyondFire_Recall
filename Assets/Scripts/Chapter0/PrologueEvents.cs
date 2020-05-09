using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PrologueEvents : MonoBehaviour {

    public GameObject objCameraSyetem;
    private CameraSystem cameraSystem;

    public GameObject Character;
    private CharacterMover characterMover;

    public GameObject objFadeEfx;
    private FadeEffect FadeEfx;

    public GameObject objTutoPanel;
    private TutorialPanel tutorialPanel;
    
    public GameObject objDialogueManager;
    private DialogueManager dialogueManager;
    //private GameObject DialogueBox;


    public GameObject objLibDialogue;
    private DialogueTrigger LibDialogue;


    public Dialogue Sujin1;
    public Dialogue JinHyung1;
    public Dialogue Sujin2;
    public Dialogue JinHyung2;

    public GameObject objLibDialogue2;
    private DialogueTrigger LibDialogue2;

    public GameObject objDoorDialogue;
    private DialogueTrigger DoorDialogue;

    public GameObject objStairDialogue;
    private DialogueTrigger StairDialogue;

    public Dialogue TutoInven;

    
    

    private void Start()
    {

        characterMover = Character.GetComponent<CharacterMover>();
        FadeEfx = objFadeEfx.GetComponent<FadeEffect>();
        dialogueManager = objDialogueManager.GetComponent<DialogueManager>();
        //DialogueBox = dialogueManager.objDialogueBox;
        LibDialogue = objLibDialogue.GetComponent<DialogueTrigger>();
        LibDialogue2 = objLibDialogue2.GetComponent<DialogueTrigger>();
        tutorialPanel = objTutoPanel.GetComponent<TutorialPanel>();

        DoorDialogue = objDoorDialogue.GetComponent<DialogueTrigger>();
        StairDialogue = objStairDialogue.GetComponent<DialogueTrigger>();

        cameraSystem = objCameraSyetem.GetComponent<CameraSystem>();
        

    }
    public IEnumerator LibraryStartEvent()
    {
        // 실내에서 시작. 카메라 반전
        cameraSystem.changeViewport();

        // 플레이어 이동상태 잠금
        characterMover.moveType = CharacterMover.MoveType.NONE;

        Character.transform.localScale = new Vector3(-1, 1, 1);
        characterMover.myAnimator.SetBool("IsWalk", false);
        characterMover.myAnimator.SetBool("IsSit", true);

        // 페이드 인 효과
        objFadeEfx.SetActive(true);
        FadeEfx.FadeIn();

        // 2초 대기
        yield return new WaitForSeconds(2.0f);

        // 수진이 플레이어를 부르는 대화 시작
        //LibDialogue.TriggerDialogue();
        dialogueManager.StartDialogue(Sujin1);
        yield return new WaitUntil(()=> dialogueManager.objDialogueBox.activeSelf == false);
        dialogueManager.StartDialogue(JinHyung1);
        yield return new WaitUntil(() => dialogueManager.objDialogueBox.activeSelf == false);
        dialogueManager.StartDialogue(Sujin2);
        yield return new WaitUntil(() => dialogueManager.objDialogueBox.activeSelf == false);
        dialogueManager.StartDialogue(JinHyung2);
        yield return new WaitUntil(() => dialogueManager.objDialogueBox.activeSelf == false);

        // 페이드 아웃 인
        FadeEfx.FadeOut();
        characterMover.myAnimator.SetBool("IsSit", false);
        yield return new WaitForSeconds(1.2f);
        FadeEfx.FadeIn();


        // 튜토리얼 패널 좌우 활성화 + 설명 다이얼로그 출력
        tutorialPanel.OnLeftRight();
        // 폰트 색 시스템 메시지 색상으로 변경
        dialogueManager.nameText.color = new Color(0.2f, 0.6f, 1);
        dialogueManager.dialogueText.color = new Color(0.2f, 0.6f, 1);
        dialogueManager.dialogueText.alignment = TextAnchor.UpperCenter;
        LibDialogue2.TriggerDialogue();
        yield return new WaitUntil(() => dialogueManager.objDialogueBox.activeSelf == false);

        tutorialPanel.OffLeftRight();
        dialogueManager.nameText.color = new Color(1, 1, 1);
        dialogueManager.dialogueText.color = new Color(1, 1, 1);
        dialogueManager.dialogueText.alignment = TextAnchor.UpperLeft;

        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;

        yield break;


    }

    public IEnumerator LibraryDoorEvent()
    {
        // 플레이어 이동상태 잠금
        characterMover.moveType = CharacterMover.MoveType.NONE;
        characterMover.myAnimator.SetBool("IsWalk", false);
        // 문 상호작용법 튜토리얼
        tutorialPanel.OnCenter();
        // 폰트 색 시스템 메시지 색상으로 변경
        dialogueManager.nameText.color = new Color(0.2f, 0.6f, 1);
        dialogueManager.dialogueText.color = new Color(0.2f, 0.6f, 1);
        dialogueManager.dialogueText.alignment = TextAnchor.UpperCenter;
        DoorDialogue.TriggerDialogue();
        yield return new WaitUntil(() => dialogueManager.objDialogueBox.activeSelf == false);
        tutorialPanel.OffCenter();
        dialogueManager.nameText.color = new Color(1, 1, 1);
        dialogueManager.dialogueText.color = new Color(1, 1, 1);
        dialogueManager.dialogueText.alignment = TextAnchor.UpperLeft;

        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
        yield break;

    }

    public IEnumerator DuplexStairEvent()
    {
        // 양방향 계단 튜토리얼
        characterMover.moveType = CharacterMover.MoveType.NONE;
        characterMover.myAnimator.SetBool("IsWalk", false);

        tutorialPanel.OnTopBottom();
        dialogueManager.nameText.color = new Color(0.2f, 0.6f, 1);
        dialogueManager.dialogueText.color = new Color(0.2f, 0.6f, 1);
        dialogueManager.dialogueText.alignment = TextAnchor.UpperCenter;
        // 튜토리얼 메시지 시작
        StairDialogue.TriggerDialogue();
        yield return new WaitUntil(() => dialogueManager.objDialogueBox.activeSelf == false);
        tutorialPanel.OffTopBottom();
        dialogueManager.nameText.color = new Color(1, 1, 1);
        dialogueManager.dialogueText.color = new Color(1, 1, 1);
        dialogueManager.dialogueText.alignment = TextAnchor.UpperLeft;

        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
        yield break;

    }

    public IEnumerator InvenTutorial()
    {
        characterMover.moveType = CharacterMover.MoveType.NONE;
        tutorialPanel.OnInven();

        dialogueManager.nameText.color = new Color(0.2f, 0.6f, 1);
        dialogueManager.dialogueText.color = new Color(0.2f, 0.6f, 1);
        dialogueManager.dialogueText.alignment = TextAnchor.UpperCenter;
        dialogueManager.StartDialogue(TutoInven);
        yield return new WaitUntil(() => dialogueManager.objDialogueBox.activeSelf == false);
        tutorialPanel.OffInven();
        dialogueManager.nameText.color = new Color(1, 1, 1);
        dialogueManager.dialogueText.color = new Color(1, 1, 1);
        dialogueManager.dialogueText.alignment = TextAnchor.UpperLeft;

        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;

        yield break;
    }
    //public IEnumerator TeachersLoungeEvent()
    //{

    //}

    //public void TeachersRoomEvent()
    //{
    //    // 인벤토리 사용 튜토리얼
    //}

    //public void B1ObjectEvent()
    //{
    //    // 오브젝트 
    //}
}
