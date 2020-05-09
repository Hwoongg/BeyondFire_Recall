using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// 사용되지 않는 컴포넌트입니다.
// GetInteraction의 트리거방식을 캐스트로 변경할 때 사용될 컴포넌트.
// 


public class RaycastSystem : MonoBehaviour {

    public GameObject Player;
    private Component playerTransform;
    private Component PlayerColl;



	void Start ()
    {
        playerTransform = Player.GetComponent<Transform>();
        PlayerColl = Player.GetComponent<Collider2D>();
	}
	

	void Update ()
    {
		
	}

    
    ////////////////////////////////////////////////////////////
    // 플레이어의 캐릭터 좌표를 기준으로 Raycast,
    // 상호작용 오브젝트가 있는지 검출
    // BoxCast 기능을 사용하도록 고려해보도록 하자
    void CastRay_forPlayer()
    {
        Vector2 rayPos = Player.transform.position;
        //RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        // transform.lossyScale 는 벡터3

        //PlayerColl.transform.
    }
}
