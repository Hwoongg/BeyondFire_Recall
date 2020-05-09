using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 카메라 두개를 운용하는 시스템 스크립트입니다.
//

// 필터 알파값 제어도 현재 스크립트에 포함합니다.
// 필터의 알파값은 외부에서 CameraSystem에 접근하여 필터 오브젝트 이미지 컴포넌트에 접근하여 직접 제어.

// TeleportSystem 컴포넌트에서 CameraSystem에 접근하여 카메라 상태를 변경 중
// 현재 FindObjectOfType<CameraSystem>() 을 통한 전역적인 접근 사용.

public class CameraSystem : MonoBehaviour
{
    
    public GameObject objPlayerInteraction; // 플레이어에게만 사용됨. find를 통해 초기화해도 무방

    // (18.07.30)
    // 캐릭터 상태 체크 컴포넌트를 별도로 생성할지 고려중입니다.
    // 어떤 객체와 닿아있는지 체크가 필요하기 때문에 GetInteraction을 확장하는게 좋을 듯
    private GetInteraction playerInteraction;

    // GUI를 통해 입력받는 제어할 두개의 카메라 오브젝트
    public GameObject playerCameraObj;
    public GameObject beyondCameraObj;

    // 두 카메라 오브젝트의 카메라 컴포넌트
    [HideInInspector]
    public Camera playerCamera;
    [HideInInspector]
    public Camera beyondCamera;

    // 필터 오브젝트 변수
    public GameObject SubcamCover;
    [HideInInspector]
    public SpriteRenderer subCamCoverRenderer;

    public GameObject PlayercamCover;
    [HideInInspector]
    public SpriteRenderer playerCamCoverRenderer;

    // 카메라 상태 변수
    public enum CameraState
    {
        IDLE,
        FOCUSING,
        SLOWMOVE,
        STOP // (181208. 지하철 계단에서 멈춤상태 필요해짐)
    }
    [HideInInspector]
    public CameraState camState;

    // 슬로우 무브상태의 카메라 도착 좌표
    private Vector3 moveDesCoord;


    private void Awake()
    {
        playerInteraction = objPlayerInteraction.GetComponent<GetInteraction>();
        playerCamera = playerCameraObj.GetComponent<Camera>();
        beyondCamera = beyondCameraObj.GetComponent<Camera>();
        subCamCoverRenderer = SubcamCover.GetComponent<SpriteRenderer>();
        playerCamCoverRenderer = PlayercamCover.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        //playerInteraction = objPlayerInteraction.GetComponent<GetInteraction>();
        //playerCamera = playerCameraObj.GetComponent<Camera>();
        //beyondCamera = beyondCameraObj.GetComponent<Camera>();
        //subCamCoverRenderer = SubcamCover.GetComponent<SpriteRenderer>();
        //playerCamCoverRenderer = PlayercamCover.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 갱신마다 서브 카메라가 이동됩니다.

        // 카메라 상태 체크
        // 플레이어 상태를 읽을지 카메라 상태를 읽을지에 대한 결정이 필요합니다
        switch (camState)
        {
            // GetInteraction.CharacterState.IDLE;
            // playerInteraction.CharacterState.IDLE;
            // 둘 다 같은 의미
            case CameraState.IDLE:
                idleMove();
                break;
            case CameraState.FOCUSING:
                focusingMove();
                break;
            case CameraState.SLOWMOVE:
                slowMove2();
                break;
            case CameraState.STOP:
                break;
        }


    }

    // //////////////////////////////////////
    // 
    // 카메라 기본 기능들
    //
    // //////////////////////////////////////

    /// <summary>
    /// 상하단 뷰포트 교환
    /// "뷰포트 교환" 이기 때문에 좌표만 교환됩니다.
    /// 카메라 자체가 교환되는것이 아님
    /// </summary>
    public void changeViewport()
    {
        Debug.Log("상 하단 뷰포트 교체");

        Rect tempRt;
        tempRt = playerCamera.rect;
        playerCamera.rect = beyondCamera.rect;
        beyondCamera.rect = tempRt;
        PlaceInfoUI placeUI = FindObjectOfType<PlaceInfoUI>();

        placeUI.objInfoText.transform.localPosition =
            new Vector3(placeUI.objInfoText.transform.localPosition.x,
            placeUI.objInfoText.transform.localPosition.y * -1,
            placeUI.objInfoText.transform.localPosition.z);
    }

    /// <summary>
    /// 층 이동에 사용될 슬로우 카메라 무브
    /// </summary>
    public IEnumerator slowMove()
    {
        // Update로 전환 가능. 상태변수 교체 뒤 시행


        Debug.Log("슬로우무브 시행");


        // 필요없을듯함
        if (camState != CameraState.SLOWMOVE)
        {
            camState = CameraState.SLOWMOVE;
        }



        float camPos = 0.0f;

        while (true)
        {
            Debug.Log("카메라 이동중...");

            beyondCameraObj.transform.position = new Vector3(beyondCameraObj.transform.position.x,
                beyondCameraObj.transform.position.y + camPos, -10);

            playerCameraObj.transform.position = new Vector3(playerCameraObj.transform.position.x,
                playerCameraObj.transform.position.y + camPos, -10);

            camPos -= 0.02f * Time.deltaTime;


            if (beyondCameraObj.transform.position.y <= -3.54f - 0.9f)
            {
                // 카메라 도착 체크
                camState = CameraState.IDLE;
                yield break;
            }
            yield return null;
        }
    }

    // 슬로우 무브 상태 정보 세팅 함수
    public void SetSlowMove(/*float startX, float startY,*/ float _endX, float _endY)
    {
        // 슬로우 무브 도착지 정보 설정
        moveDesCoord.x = _endX;
        moveDesCoord.y = _endY + 0.9f;
        moveDesCoord.z = -10;

        // 슬로우 무브 상태로 전환
        camState = CameraState.SLOWMOVE;
    }
    // Update 방식
    public void slowMove2()
    {
        // 카메라의 현재 좌표를 목적지 방향으로 갱신
        playerCameraObj.transform.position = Vector3.MoveTowards(playerCameraObj.transform.position, moveDesCoord, 0.7f * Time.deltaTime);


        // 이동 완료 후 IDLE 상태로 복귀
        if(playerCameraObj.transform.position == moveDesCoord)
        {
            camState = CameraState.IDLE;
            Debug.Log("슬로우 무브 완료");
        }
    }

    // ///////////////////////////////////
    //
    // 실행될 카메라 이벤트 함수들
    //
    // ///////////////////////////////////

    /// <summary>
    /// 기본 상태
    /// </summary>
    public void idleMove()
    {
        // 서브카메라를 플레이어의 상대좌표에 위치시킵니다. 
        // 플레이어와 x좌표 동기화, y좌표는 상대좌표 +2.7f. 
        beyondCameraObj.transform.position = new Vector3(objPlayerInteraction.transform.position.x, 
            objPlayerInteraction.transform.position.y + 2.7f, -10);

        //playerCameraObj.transform.position = new Vector3(player.transform.position.x,
        //    player.transform.position.y + 0.85f, -10);

        // 좌표의 음양 경계에서 꼬임현상 발생
        playerCameraObj.transform.position = new Vector3(objPlayerInteraction.transform.position.x,
            objPlayerInteraction.transform.position.y - (objPlayerInteraction.transform.position.y % 1.8f) - 0.9f, -10);

        //playerCameraObj.transform.position = new Vector3(player.transform.position.x,
        //    (player.transform.position.y + 1.8f) - ((player.transform.position.y + 1.8f) % 1.8f) - 0.9f, -10);


        //Debug.Log("Camera is IdleState");
        
    }

    /// <summary>
    /// 문에 접근했을 때
    /// </summary>
    public void focusingMove()
    {
        //
        // (18.08.08) onDoorMove()의 이름 변경.
        //


        // 이동될 문의 좌표로 서브 카메라의 좌표 고정. 외부에서 doorCoord를 입력받습니다.
        // (18.07.30) GetInteraction 컴포넌트에서 획득된 객체의 TeleportSystem의 컴포넌트 도착지 좌표를 사용. 
        // InteractionSystem의 자료형으로 조회 가능한지 테스트 필요.
        // -> 불가능, TeleportSystem의 자료형으로 참조해야한다.

        // playerInteraction의 현재 닿아있는 오브젝트의 TeleportSystem 컴포넌트 참조...
        TeleportSystem telSystem = playerInteraction.triggerInteraction.GetComponent<TeleportSystem>();

        // 참조한 TeleprtSystem 컴포넌트의 도착지 좌표를 대입한다! y값은 +2.7f 상대좌표
        //beyondCameraObj.transform.position = new Vector3(telSystem.Destination.transform.position.x,
        //    telSystem.Destination.transform.position.y + 0.9f, -10);
        beyondCameraObj.transform.position = new Vector3(telSystem.Destination.transform.position.x,
            telSystem.Destination.transform.position.y - (telSystem.Destination.transform.position.y % 1.8f) - 0.9f, -10);


        playerCameraObj.transform.position = new Vector3(objPlayerInteraction.transform.position.x,
            objPlayerInteraction.transform.position.y - (objPlayerInteraction.transform.position.y % 1.8f) - 0.9f, -10);

        //playerCameraObj.transform.position = new Vector3(player.transform.position.x,
        //    (player.transform.position.y + 1.8f) - ((player.transform.position.y + 1.8f) % 1.8f) - 0.9f, -10);

        // 현재 닿은 문과의 좌표계산, 필터의 알파값 제어 부분입니다
        // ....
    }

    public void SetStop()
    {
        camState = CameraState.STOP;
    }
}
