using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoorInteraction : TeleportSystem {

    public GameObject DoorCover;

    new void Start()
    {
        base.Start();

        if (DoorCover == null)
        {
            if (transform.childCount > 0)
            {
                DoorCover = transform.GetChild(0).gameObject;
            }
        }
    }

    public override void doAction()
    {
        if (characterMover.moveType == CharacterMover.MoveType.LOCK) // 다중입력 방지용 예외처리 코드 입니다. 쓸데없는 루틴 방지, 애니메이터 꼬임 제거
        {
            Debug.Log("이동명령 다중 입력 방지 코드 실행");
            return;
        }

        Debug.Log("TeleportSystem의 doAction()이 실행됨");


        //StartCoroutine("DoorMoveTeleport");
        StartCoroutine("NormalDoorRoutine");

    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    IEnumerator NormalDoorRoutine()
    {
        // 캐릭터를 커멘드 입력 불가상태로 세팅한다.
        characterMover.moveType = CharacterMover.MoveType.LOCK;

        yield return StartCoroutine("MoveCenter");
        yield return StartCoroutine("DoorOpen");

        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
        yield break;
    }

    public IEnumerator DoorOpen()
    {

        float nowLerp = 0.0f;
        float nowDoorXScale;
        float startDoorXScale = DoorCover.transform.localScale.x;
        float endDoorXScale =  startDoorXScale * 0.2f;
        float moveTime = 1.0f;
        nowDoorXScale = Mathf.Lerp(startDoorXScale, endDoorXScale, nowLerp);

        // 애니메이터 뒷모습으로 전환
        Animator animator = TeleportCharacter.GetComponent<Animator>();
        animator.SetBool("IsBack", true);
        yield return new WaitForSeconds(0.3f);

        while (true)
        {
            nowDoorXScale = Mathf.Lerp(startDoorXScale, endDoorXScale, nowLerp);
            DoorCover.transform.localScale = new Vector3(nowDoorXScale, DoorCover.transform.localScale.y, DoorCover.transform.localScale.z);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 이동완료
                nowLerp += Time.deltaTime / moveTime;
            else
            {
                yield return new WaitForSeconds(0.3f);
                Teleport();
                cameraSystem.changeViewport();
                animator.SetBool("IsBack", false);
                DoorCover.transform.localScale = new Vector3(startDoorXScale, DoorCover.transform.localScale.y, DoorCover.transform.localScale.z);
                yield break;
            }

            yield return null;
        }
    }
}
