using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// 특정한 오브젝트를 지정한 좌표로 이동시키는 기능 스크립트입니다.
//

// (18.07.30) 카메라 오브젝트를 참조하여 카메라 오브젝트의 상태, 내부함수를 사용하여 작동하는 방법 생각중...
// 카메라가 TeleportSystem 컴포넌트 오브젝트와만 상호작용한다면 괜찮은 방법일 듯
// CameraState를 생성하여 관리하게될 것이기 때문에 TeleportSystem은 카메라를 제어하는 오브젝트임이 확실해야함...
// CameraSystem이 싱글턴이 되는게 편하다고 생각됨.


public class TeleportSystem : InteractionSystem
{
    // 목적지 좌표. (18.07.19) public에서 private로 변경,
    //public Vector2 TeleportPosition; // 아래 목적지 오브젝트에서 읽어 수행
    private Transform DesTransform; // Destination 오브젝트의 transform 멤버에 이미 존재. 컴포넌트 획득할 필요 없음

    // 목적지 오브젝트
    public GameObject Destination;

    public string InfoText;

    // 이동시킬 캐릭터. findTag 활용하여 자동화 가능
    // 트리거 체크하여 닿아있는 오브젝트를 이동시키도록 할 수도 있음.
    // 현재는 플레이어만 이동하도록 만들어짐.
    protected GameObject TeleportCharacter;
    protected CharacterMover characterMover; // 캐릭터 이동상태 제어용
    protected Animator characterAnimator;

    // 카메라 시스템 제어를 위한 변수...
    // 카메라를 제어하는 오브젝트 종류가 많아질 경우 싱글턴으로 교체하는 방안 필요.
    protected CameraSystem cameraSystem;

    public void Start()
    {
        TeleportCharacter = GameObject.FindGameObjectWithTag("Player");
        characterMover = TeleportCharacter.GetComponent<CharacterMover>();
        DesTransform = Destination.GetComponent<Transform>();
        cameraSystem = GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>();
        characterAnimator = TeleportCharacter.GetComponent<Animator>();

    }


    public override void doAction()
    {
        if (characterMover.moveType == CharacterMover.MoveType.NONE) // 다중입력 방지용 예외처리 코드 입니다. 쓸데없는 루틴 방지, 애니메이터 꼬임 제거
        {
            Debug.Log("이동명령 다중 입력 방지 코드 실행");
            return;
        }

        Debug.Log("TeleportSystem의 doAction()이 실행됨");

        // 캐릭터를 커멘드 입력 불가상태로 세팅한다.
        //characterMover.moveType = CharacterMover.MoveType.NONE;


        //StartCoroutine("DoorMoveTeleport");
        StartCoroutine("TeleportRoutine");

    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    // 캐릭터의 좌표를 도착지 좌표로 변경시키는 함수
    public void Teleport()
    {
        // 도착지로 입력된 오브젝트의 좌표로 값을 대입하여 이동
        //TeleportCharacter.transform.position = new Vector3(TeleportPosition.x, TeleportPosition.y, 0);
        TeleportCharacter.transform.position = new Vector3(DesTransform.position.x, DesTransform.position.y + 0.035f, DesTransform.position.z);
        //TeleportCharacter.transform.position = new Vector3(DesTransform.position.x, (DesTransform.position.y + 1.8f) - ((DesTransform.position.y + 1.8f) % 1.8f) - 1.8f + 0.05f, DesTransform.position.z);

        TeleportCharacter.transform.position = new Vector3(DesTransform.position.x,
            DesTransform.position.y - (DesTransform.position.y % 1.8f) - 1.8f + 0.035f,
            DesTransform.position.z);

    }


    // 문 이동시 시행될 전체 업데이트. 구형모델. TotalMove로 개선
    private IEnumerator DoorMoveTeleport()
    {
        //Animator animator = TeleportCharacter.GetComponent<Animator>();

        // 캐릭터 애니메이터 걷기 세팅
        characterAnimator.SetBool("IsWalk", true);
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

            // 캐릭터 좌표 이동. 1.0f의 속도로
            TeleportCharacter.transform.position = Vector2.MoveTowards(TeleportCharacter.transform.position, desCoord, 0.7f * Time.deltaTime);

            // 캐릭터 방향은 설정된 방향벡터로
            TeleportCharacter.transform.localScale = dirVec;



            // 도착시
            if (TeleportCharacter.transform.position.x == this.transform.position.x)
            {
                // 애니메이터 뒷모습으로 전환
                characterAnimator.SetBool("IsWalk", false);
                characterAnimator.SetBool("IsBack", true);

                // 1.5초 뒤 이동, 이동타입 회복, 뷰포트전환
                yield return new WaitForSeconds(1.5f);
                Teleport();
                characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
                cameraSystem.changeViewport();

                // 애니메이터 IDLE로 회복
                characterAnimator.SetBool("IsBack", false);
                characterAnimator.SetBool("IsWalk", true);

                yield break;
            }
            yield return null;
        }


    }

    // 중앙에 가서 뒤돌아 이동하는 루틴
    public IEnumerator TeleportRoutine()
    {
        // 캐릭터를 커멘드 입력 불가상태로 세팅한다.
        characterMover.moveType = CharacterMover.MoveType.NONE;

        yield return StartCoroutine("MoveCenter");

        Teleport();
        cameraSystem.changeViewport();

        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;

        yield break;
    }
    public IEnumerator MoveCenter()
    {
        //Animator animator = TeleportCharacter.GetComponent<Animator>();

        // 캐릭터 애니메이터 걷기 세팅
        characterAnimator.SetBool("IsWalk", true);
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

            // 캐릭터 좌표 이동. 1.0f의 속도로
            TeleportCharacter.transform.position = Vector2.MoveTowards(TeleportCharacter.transform.position, desCoord, 0.7f * Time.deltaTime);

            // 캐릭터 방향은 설정된 방향벡터로
            TeleportCharacter.transform.localScale = dirVec;
            characterMover.Icon.transform.localScale = new Vector3(dirVec.x, 1, 1);


            // 도착시
            if (TeleportCharacter.transform.position.x == this.transform.position.x)
            {
                // 애니메이터 걷기 멈춤
                characterAnimator.SetBool("IsWalk", false);

                yield break;
            }
            yield return null;
        }
    }
    public IEnumerator WaitTurnTeleport()
    {
        // 애니메이터 뒷모습으로 전환
        //Animator animator = TeleportCharacter.GetComponent<Animator>();
        characterAnimator.SetBool("IsBack", true);
        //characterMover.moveType = CharacterMover.MoveType.NONE;

        // 1.5초 뒤 이동, 이동타입 회복, 뷰포트전환
        yield return new WaitForSeconds(1.5f);
        Teleport();

        // 무브상태회복
        //characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;
        cameraSystem.changeViewport();
        characterAnimator.SetBool("IsBack", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 카메라 상태 FOCUSING 으로
        if (!collision.CompareTag("GetInteraction"))
            return;
        cameraSystem.camState = CameraSystem.CameraState.FOCUSING;
        Debug.Log("카메라 FOCUSING 상태로 전환");

        // 임시용 커버 비활성화 코드
        //cameraSystem.SubcamCover.SetActive(false);

        // 거리 차이 계산, 필터 알파값 비례 조정
        // 거리가 0.6f일때 알파값 0, 0일때 255
        // 0.6 / 0.6 * 255 = 255
        // 0 / 0.6 * 255 = 0
        // 거리 / 0.6 * 255의 절대값을 알파값에서 뺀다.
        float range = transform.position.x - TeleportCharacter.transform.position.x;

        cameraSystem.subCamCoverRenderer.color = new Color(1, 1, 1, Mathf.Abs(range / 0.6f));

        FindObjectOfType<PlaceInfoUI>().TextOn(InfoText);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cameraSystem.camState = CameraSystem.CameraState.IDLE;
        Debug.Log("카메라 IDLE 상태로 전환");

        //cameraSystem.SubcamCover.SetActive(true);
        // 필터 상태 복귀
        // ...
        cameraSystem.subCamCoverRenderer.color = new Color(1, 1, 1, 1);

        FindObjectOfType<PlaceInfoUI>().TextOff();
    }

    public IEnumerator FadeOutRoutine()
    {
        float nowLerp = 0.0f;
        float nowAlpha;

        while (true)
        {
            nowAlpha = Mathf.Lerp(1, 0, nowLerp);

            cameraSystem.playerCamCoverRenderer.color = new Color(1, 1, 1, 1 - nowAlpha);
            //cameraSystem.subCamCoverRenderer.color = new Color(1, 1, 1, nowAlpha);
            //TeleportCharacter.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, nowAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }

        //cameraSystem.playerCamCoverRenderer.color = new Color(1, 1, 1, 0);
        //cameraSystem.subCamCoverRenderer.color = new Color(1, 1, 1, 1);
        //TeleportCharacter.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        yield break;
    }

    public IEnumerator FadeInRoutine()
    {
        float nowLerp = 0.0f;
        float nowAlpha;

        while (true)
        {
            nowAlpha = Mathf.Lerp(1, 0, nowLerp);

            cameraSystem.playerCamCoverRenderer.color = new Color(1, 1, 1, nowAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }

        yield break;
    }
}
