using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 자신이 속한 오브젝트의 Rotate 값에 따른 자동이동을 수행하는 스크립트 입니다.
// [rotate] 
// 180 : 좌측 (-1)
// 0 : 우측 (+1)
//

public class AutoMoving : MonoBehaviour
{

    public float moveSpeed;


    void Update ()
    {
        float x = 1 * Time.deltaTime * moveSpeed;
        gameObject.transform.Translate(x, 0, 0);
	}
}
