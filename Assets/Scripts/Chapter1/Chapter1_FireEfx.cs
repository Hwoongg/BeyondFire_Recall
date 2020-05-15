using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 챕터1 전용 화염 스크립트입니다.
// 생성될때마다 챕터 관리자 클래스에게 자신을 등록합니다.
// 불의 단계가 점차 줄어드는 기능이 추가됩니다.
//
public class Chapter1_FireEfx : FireEfx {

    //Chapter1Manager c1Manager;


    new private void Start()
    {
        base.Start();
        Debug.Log("챕터1용 화염 스크립트 시작 감지");
        //c1Manager = FindObjectOfType<Chapter1Manager>();
        FindObjectOfType<Chapter1Manager>().FireList.Add(this);
    }

    // 불길 레벨 상승루틴 중단 후 하강루틴으로 전환 
    public void LullFire()
    {
        StopCoroutine(nowRoutine);
        StartCoroutine(FireLullRoutine());
    }

    IEnumerator FireLullRoutine()
    {
        while(true)
        {
            
            yield return new WaitForSeconds(delay);
            //Transform[] childList = transform.GetComponentsInChildren<Transform>();
            for(int i=0; i<4;i++)
            {


                transform.GetChild(i).gameObject.SetActive(false);

            }
            
            transform.GetChild(nowLevel).gameObject.SetActive(true);
            //childList[nowLevel].gameObject.SetActive(true);

            if (nowLevel == 0)
                break;

            nowLevel--;

            

            yield return null;
        }

        yield break;
    }
}
