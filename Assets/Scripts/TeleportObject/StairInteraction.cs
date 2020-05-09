using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// 단방향 계단에서 사용될 상호작용 스크립트 입니다
//

// (18.08.07)
// 화면 센터 버튼이 두개로 분할될 것.
// 눌린 버튼에 지정된 계단으로 이동되도록 처리해야함.
// 위 아래 버튼 모두 doAction() 함수를 실행할 것이므로 해당 함수 내에서 구분하는 방법을 찾도록 한다.

public class StairInteraction : TeleportSystem
{

    public override void doAction()
    {
        if (characterMover.moveType == CharacterMover.MoveType.NONE) // 다중입력 방지용 예외처리 코드 입니다. 쓸데없는 루틴 방지
        {
            Debug.Log("이동명령 다중 입력 방지 코드 실행");
            return;
        }

        Debug.Log("StairInteraction의 doAction()이 실행됨");

        // 캐릭터를 커멘드 입력 불가상태로 세팅한다.
        characterMover.moveType = CharacterMover.MoveType.NONE;
        StartCoroutine("StairMoveTeleport");

        //StartCoroutine(StairRoutine());

    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    // 카메라 도착을 기다린 후 좌표이동 실시하는 루틴
    IEnumerator CamWaitTeleport()
    {

        while (true)
        {
            if (cameraSystem.camState == CameraSystem.CameraState.IDLE)
            {
                //TeleportCharacter.SetActive(true); // ★
                TeleportCharacter.GetComponent<SpriteRenderer>().enabled = true;
                Teleport();
                characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
                Debug.Log("계단 다중입력 체크용 로그입니다...");
                yield break;
            }

            yield return null;
        }
    }


    // 계단 이동시 시행될 전체 업데이트
    private IEnumerator StairMoveTeleport()
    {
        Animator animator = TeleportCharacter.GetComponent<Animator>();

        // 캐릭터 애니메이터 걷기 세팅
        animator.SetBool("IsWalk", true);
        Vector2 desCoord;
        desCoord.x = this.transform.position.x;
        desCoord.y = TeleportCharacter.transform.position.y;

        // 캐릭터 방향 설정
        Vector3 dirVec;

        if (TeleportCharacter.transform.position.x < this.transform.position.x)
            dirVec = new Vector3(-1, 1, 1);
        else
            dirVec = new Vector3(1, 1, 1);

        while (true)
        {

            // 캐릭터 좌표 이동. 1.0f의 속도로.
            TeleportCharacter.transform.position = Vector2.MoveTowards(TeleportCharacter.transform.position, desCoord, 0.5f * Time.deltaTime);


            TeleportCharacter.transform.localScale = dirVec;
            characterMover.Icon.transform.localScale = new Vector3(dirVec.x, 1, 1);

            // 도착시
            if (TeleportCharacter.transform.position.x == this.transform.position.x)
            {
                // 애니메이터 뒷모습으로 전환
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsDoorIn", true);

                // 1.5초 뒤 이동, 이동타입 회복, 뷰포트전환
                yield return new WaitForSeconds(1.0f);
                //TeleportCharacter.SetActive(false); // ★
                TeleportCharacter.GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.5f);


                // 카메라슬로우 무브 상태로 세팅한 후
                cameraSystem.SetSlowMove(Destination.transform.position.x, Destination.transform.position.y);

                PlaceInfoUI placeUI = FindObjectOfType<PlaceInfoUI>();
                placeUI.TextOn(InfoText);
                yield return StartCoroutine(placeUI.TextFadeInRoutine());
                // 코루틴 내부의 코루틴 생명주기 파악 필요함
                yield return StartCoroutine("CamWaitTeleport");

                // 애니메이터 IDLE로 회복
                animator.SetBool("IsDoorIn", false);
                animator.SetBool("IsWalk", true);

                yield return new WaitForSeconds(1.0f);
                yield return StartCoroutine(placeUI.TextFadeOutRoutine());
                //yield return StartCoroutine(FindObjectOfType<PlaceInfoUI>().TextFadeRoutine(InfoText));

                yield break;
            }
            yield return null;
        }


    }

    public IEnumerator StairRoutine()
    {
        characterMover.moveType = CharacterMover.MoveType.NONE;

        yield return StartCoroutine(MoveCenter());

        // 카메라슬로우 무브 상태로 세팅한 후
        cameraSystem.SetSlowMove(Destination.transform.position.x, Destination.transform.position.y);

        yield return StartCoroutine(CamWaitTeleport());

        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;

        yield return StartCoroutine(FindObjectOfType<PlaceInfoUI>().TextFadeRoutine(InfoText));

        yield break;
    }

    // 아무것도 안하게 하기 위한 재정의
    private void OnTriggerStay2D(Collider2D collision)
    {
        return;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        return;
    }
}
