using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorInteraction : TeleportSystem
{

    private GameObject DoorCover;

    //public string InfoText;

    new void Start()
    {
        base.Start();
        DoorCover = transform.GetChild(0).gameObject;
    }

    public override void doAction()
    {
        if (characterMover.moveType == CharacterMover.MoveType.NONE) // 다중입력 방지용 예외처리 코드 입니다. 쓸데없는 루틴 방지, 애니메이터 꼬임 제거
        {
            Debug.Log("이동명령 다중 입력 방지 코드 실행");
            return;
        }

        Debug.Log("TeleportSystem의 doAction()이 실행됨");


        //StartCoroutine("DoorMoveTeleport");
        StartCoroutine("SlidingDoorRoutine");

    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    // 미닫이 문 전체 동작 코루틴
    IEnumerator SlidingDoorRoutine()
    {
        // 캐릭터를 커멘드 입력 불가상태로 세팅한다.
        characterMover.moveType = CharacterMover.MoveType.NONE;

        yield return StartCoroutine("MoveCenter");
        yield return StartCoroutine("DoorSlide");

        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
        yield break;
    }

    // 미닫이 문열기 동작 루틴
    public IEnumerator DoorSlide()
    {

        float nowLerp = 0.0f;
        float nowDoorXPos;
        float startDoorXPos = DoorCover.transform.position.x;
        float endDoorXPos = startDoorXPos + (-DoorCover.transform.localScale.x * 0.8f);
        float moveTime = 1.0f;
        nowDoorXPos = Mathf.Lerp(startDoorXPos, endDoorXPos, nowLerp);

        // 애니메이터 뒷모습으로 전환
        Animator animator = TeleportCharacter.GetComponent<Animator>();
        animator.SetBool("IsBack", true);
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(FadeOutRoutine());

        while (true)
        {
            nowDoorXPos = Mathf.Lerp(startDoorXPos, endDoorXPos, nowLerp);
            DoorCover.transform.position = new Vector3(nowDoorXPos, DoorCover.transform.position.y, DoorCover.transform.position.z);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 이동완료
                nowLerp += Time.deltaTime / moveTime;
            else
            {

                Teleport();
                cameraSystem.changeViewport();
                animator.SetBool("IsBack", false);
                DoorCover.transform.position = new Vector3(startDoorXPos, DoorCover.transform.position.y, DoorCover.transform.position.z);

                yield return StartCoroutine(FadeInRoutine());

                yield break;
            }

            yield return null;
        }

    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    // 카메라 상태 FOCUSING 으로
    //    cameraSystem.camState = CameraSystem.CameraState.FOCUSING;
    //    Debug.Log("카메라 FOCUSING 상태로 전환");

    //    // 임시용 커버 비활성화 코드
    //    //cameraSystem.SubcamCover.SetActive(false);

    //    // 거리 차이 계산, 필터 알파값 비례 조정
    //    // 거리가 0.6f일때 알파값 0, 0일때 255
    //    // 0.6 / 0.6 * 255 = 255
    //    // 0 / 0.6 * 255 = 0
    //    // 거리 / 0.6 * 255의 절대값을 알파값에서 뺀다.
    //    float range = transform.position.x - TeleportCharacter.transform.position.x;

    //    cameraSystem.subCamCoverRenderer.color = new Color(1, 1, 1, Mathf.Abs(range / 0.6f));

    //    FindObjectOfType<PlaceInfoUI>().TextOn(InfoText);

    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    cameraSystem.camState = CameraSystem.CameraState.IDLE;
    //    Debug.Log("카메라 IDLE 상태로 전환");

    //    //cameraSystem.SubcamCover.SetActive(true);
    //    // 필터 상태 복귀
    //    // ...

    //    FindObjectOfType<PlaceInfoUI>().TextOff();

    //}
}
