using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletDoorInteraction : NormalDoorInteraction
{

    public override void doAction()
    {
        if (characterMover.moveType == CharacterMover.MoveType.NONE) // 다중입력 방지용 예외처리 코드 입니다. 쓸데없는 루틴 방지, 애니메이터 꼬임 제거
        {
            Debug.Log("이동명령 다중 입력 방지 코드 실행");
            return;
        }

        Debug.Log("TeleportSystem의 doAction()이 실행됨");


        //StartCoroutine("DoorMoveTeleport");
        StartCoroutine("ToiletDoorRoutine");

    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    IEnumerator ToiletDoorRoutine()
    {
        // 캐릭터를 커멘드 입력 불가상태로 세팅한다.
        characterMover.moveType = CharacterMover.MoveType.NONE;

        yield return StartCoroutine("MoveCenter");
        yield return StartCoroutine("DoorOpen");

        // ////////////////////////////////////////
        // 문 Scale 반대방향으로 자동이동
        // ////////////////////////////////////////

        // 이동 애니메이션 설정
        Animator animator = TeleportCharacter.GetComponent<Animator>();
        animator.SetBool("IsWalk", true);

        // 걸어갈 방향 설정
        float direction = -DoorCover.transform.localScale.x ;

        // 도착지 설정
        Vector2 AfterMoveCoord = new Vector2(Destination.transform.position.x + (1.0f * direction), 
            TeleportCharacter.transform.position.y);
        
        // 이동방향으로 캐릭터 방향 교정
        TeleportCharacter.transform.localScale = new Vector3(-direction, 1, 1);

        yield return new WaitForSeconds(0.3f);
        // 좌표이동
        while (true)
        {
            TeleportCharacter.transform.position =
                Vector2.MoveTowards(TeleportCharacter.transform.position, AfterMoveCoord, 0.7f * Time.deltaTime);

            if (TeleportCharacter.transform.position.x == AfterMoveCoord.x)
            {
                animator.SetBool("IsWalk", false);
                break;
            }
            yield return null;
        }

        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
        yield break;
    }
}
