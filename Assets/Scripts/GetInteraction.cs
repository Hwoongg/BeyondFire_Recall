using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 자신이 속한 오브젝트와 트리거가 발생한 오브젝트를 참조.
// InteractionSystem 컴포넌트를 가져와 실행가능한 스크립트입니다.
// 트리거 이벤트는 현재 컴포넌트가 속해있는 오브젝트의 충돌체에서 발생합니다.

// (18.07.30) 트리거 이벤트를 통해 제어될 캐릭터 상태변수 추가. CameraSystem 컴포넌트에서 사용됩니다.

public class GetInteraction : MonoBehaviour
{

    public GameObject Character;

    // 트리거 발생된 오브젝트와 상호작용 시스템 컴포넌트
    private GameObject triggerObj;

    [HideInInspector]
    public InteractionSystem triggerInteraction; // (18.07.30) public 전환, 카메라쪽에서 사용됩니다.

    // (18.07.30) 추가된 캐릭터의 상태 변수. 트리거 이벤트를 통한 제어
    //public enum CharacterState
    //{
    //    IDLE,
    //    ONDOOR,
    //}
    //public CharacterState charState; // (18.08.06) 카메라 시스템을 참조하는 형식으로 변경 예정


    private void Start()
    {
        //charState = CharacterState.IDLE; // (18.08.06) 카메라 상태 삭제 예정
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ////////////////////////////////
        // 현우형이 추가해준 코드
        //if(!collision.CompareTag("Door"))
        //{
        //    return;
        //}
        // ///////////////////////////////

        //// collision에 초기화된 오브젝트를 확인
        //if (collision.CompareTag("Fire"))
        //    return;
        triggerObj = collision.gameObject;
        
        
        //Debug.Log("GetInteraction이 " + triggerObj.name + "를 참조");

        // (18.07.30) doAction() 부분에서 옮겨옴. 카메라에서 사용되기 위함
        triggerInteraction = triggerObj.GetComponent<InteractionSystem>();

        //charState = CharacterState.ONDOOR; // (18.08.06) 카메라 상태 삭제 예정
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        triggerObj = collision.gameObject;
        triggerInteraction = triggerObj.GetComponent<InteractionSystem>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ////////////////////////////////
        // 현우형이 추가해준 코드
        //if (!collision.CompareTag("Door") || collision.gameObject != triggerObj)
        //{
        //    return;
        //}
        // ////////////////////////////////
        
        //Debug.Log("GetInteraction의" + triggerObj.name + "참조 해제");
        triggerObj = null;
        //charState = CharacterState.IDLE; // (18.08.06) 카메라 상태 삭제 예정
    }

    public void doAction()
    {
        //Debug.Log("GetInteraction의 doAction() 실행.");
        if(triggerObj==null)
        {
            Debug.Log("참조된 상호작용 오브젝트가 없습니다");
            return;
        }
        triggerInteraction.doAction();

        Debug.Log(triggerObj.gameObject.name);
    }

    public void upAction()
    {
        //Debug.Log("GetInteraction의 doAction() 실행.");
        if (triggerObj == null)
        {
            Debug.Log("참조된 상호작용 오브젝트가 없습니다");
            return;
        }
        triggerInteraction.upAction();

        Debug.Log(triggerObj.gameObject.name);
    }
    public void downAction()
    {
        //Debug.Log("GetInteraction의 doAction() 실행.");
        if (triggerObj == null)
        {
            Debug.Log("참조된 상호작용 오브젝트가 없습니다");
            return;
        }
        triggerInteraction.downAction();

        Debug.Log(triggerObj.gameObject.name);
    }
}
