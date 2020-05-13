using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayDownStair : TeleportSystem
{

    public override void doAction()
    {
        if(characterMover.moveType == CharacterMover.MoveType.LOCK)
        {
            Debug.Log("이동명령 다중 입력 방지 코드 실행");
            return;
        }

        StartCoroutine(Routine());
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    IEnumerator Routine()
    {
        // 캐릭터를 커멘드 입력 불가상태로 세팅한다.
        characterMover.moveType = CharacterMover.MoveType.LOCK;

        yield return StartCoroutine(MoveCenter());
        yield return StartCoroutine(StairDownMove());

        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
        yield break;
    }

    // 현재 오브젝트 좌표의 (-0.55, -0.35) 좌표까지 이동.
    IEnumerator StairDownMove()
    {
        characterAnimator.SetBool("IsWalk", true);

        float nowLerp = 0.0f;
        Vector3 nowPlayerCoord;
        Vector3 startPlayerCoord = gameObject.transform.position;
        Vector3 endPlayerCoord = startPlayerCoord;
        endPlayerCoord.x -= 0.55f;
        endPlayerCoord.y -= 0.35f;

        StartCoroutine(FadeOutRoutine());

        while (true)
        {
            nowPlayerCoord = Vector3.Lerp(startPlayerCoord, endPlayerCoord, nowLerp);
            TeleportCharacter.transform.position = nowPlayerCoord;
            TeleportCharacter.GetComponent<SpriteRenderer>().color =
                new Color(1, 1, 1, 1 - nowLerp);

            cameraSystem.camState = CameraSystem.CameraState.STOP;

            if (nowLerp < 1.0f)
                nowLerp += Time.deltaTime / 1.0f;
            else
            {
                characterAnimator.SetBool("IsWalk", false);

                Teleport();
                cameraSystem.changeViewport();

                TeleportCharacter.GetComponent<SpriteRenderer>().color =
                new Color(1, 1, 1, 1);

                cameraSystem.camState = CameraSystem.CameraState.IDLE;
                yield return StartCoroutine(FadeInRoutine());


                yield break;
            }

            yield return null;
        }

        yield break;
    }
}
