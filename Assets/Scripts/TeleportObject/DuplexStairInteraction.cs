using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// 양방향 계단에서 사용될 상호작용 스크립트 입니다
//

public class DuplexStairInteraction : StairInteraction
{
    // 목적지 오브젝트 추가
    public GameObject Destination2;
    public string InfoText2;

    // 목적지 2로 이동
    public void Teleport2()
    {
        // 도착지로 입력된 오브젝트의 좌표로 값을 대입하여 이동
        //TeleportCharacter.transform.position = new Vector3(TeleportPosition.x, TeleportPosition.y, 0);
        TeleportCharacter.transform.position = new Vector3(Destination2.transform.position.x, Destination2.transform.position.y + 0.035f, Destination2.transform.position.z);
    }
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

        // 캐릭터 일시적으로 사라짐. 렌더러 비활성화. 현재 사양에 없음
        // ...

        //// 슬로우 무브 상태로 세팅한 후
        //cameraSystem.SetSlowMove(Destination.transform.position.x, Destination.transform.position.y);

        //// 코루틴을 돌려 도착이 감지되면 이동을 시행한다
        //StartCoroutine("CamWaitTeleport");

        StartCoroutine("StairMoveTeleport");
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        if (characterMover.moveType == CharacterMover.MoveType.NONE) // 다중입력 방지용 예외처리 코드 입니다. 쓸데없는 루틴 방지
        {
            Debug.Log("이동명령 다중 입력 방지 코드 실행");
            return;
        }

        Debug.Log("StairInteraction의 doAction()이 실행됨");

        // 캐릭터를 커멘드 입력 불가상태로 세팅한다.
        characterMover.moveType = CharacterMover.MoveType.NONE;

        // 캐릭터 일시적으로 사라짐. 렌더러 비활성화. 현재 사양에 없음
        // ...

        //// 슬로우 무브 상태로 세팅한 후
        //cameraSystem.SetSlowMove(Destination.transform.position.x, Destination.transform.position.y);

        //// 코루틴을 돌려 도착이 감지되면 이동을 시행한다
        //StartCoroutine("CamWaitTeleport");

        StartCoroutine("StairMoveTeleport2");
    }

    private IEnumerator StairMoveTeleport2()
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
                cameraSystem.SetSlowMove(Destination2.transform.position.x, Destination2.transform.position.y);

                PlaceInfoUI placeUI = FindObjectOfType<PlaceInfoUI>();
                placeUI.TextOn(InfoText2);
                yield return StartCoroutine(placeUI.TextFadeInRoutine());

                // 코루틴 내부의 코루틴 생명주기 파악 필요함
                yield return StartCoroutine("CamWaitTeleport2");

                // 애니메이터 IDLE로 회복
                animator.SetBool("IsDoorIn", false);
                animator.SetBool("IsWalk", true);

                yield return new WaitForSeconds(1.0f);
                yield return StartCoroutine(placeUI.TextFadeOutRoutine());

                yield break;
            }
            yield return null;
        }


    }

    IEnumerator CamWaitTeleport2()
    {

        while (true)
        {
            if (cameraSystem.camState == CameraSystem.CameraState.IDLE)
            {
                TeleportCharacter.GetComponent<SpriteRenderer>().enabled = true;
                Teleport2();
                characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
                Debug.Log("계단 다중입력 체크용 로그입니다...");
                yield break;
            }

            yield return null;
        }
    }
}
