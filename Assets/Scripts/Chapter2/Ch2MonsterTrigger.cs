using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 접촉 시 괴물 등장 이벤트 발생하는 트리거 스크립트
// 괴물을 가리고 있는 패널의 알파값이 하강, 사운드 출력.
//

public class Ch2MonsterTrigger : MonoBehaviour
{
    public GameObject objMonsterPannel;
    public GameObject objMonster;
    //public GameObject objMonstarEffect;
    private PannelController pannelController;

    private CharacterMover characterMover;

    IEnumerator monsterControl;

    bool isActive;
    //AudioSource AudioSource;


    private void Start()
    {
        //AudioSource = GetComponent<AudioSource>();
        characterMover = FindObjectOfType<CharacterMover>();
        monsterControl = MonsterControl();
        isActive = true;
        pannelController = new PannelController();
        pannelController.pannelRenderer = objMonsterPannel.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            StartCoroutine(MainRoutine());
            isActive = false;
        }
    }

    IEnumerator MainRoutine()
    {
        // 이동 불가 상태
        characterMover.moveType = CharacterMover.MoveType.NONE;
        characterMover.myAnimator.SetBool("IsWalk", false);

        //// 괴물 움직임 코루틴 시작
        //StartCoroutine(monsterControl);
        
        
        for(int i = 0; i<3; i++)
        {
            //StartCoroutine(MonsterControl());
            objMonster.GetComponent<Rigidbody2D>().AddForce(new Vector3(200, 0, 0));
            //AudioSource.Play();
            yield return StartCoroutine(pannelController.CustomFade(0.5f, 0.2f));

            // 괴물 움직임
            //objMonster.GetComponent<Rigidbody2D>().AddForce(new Vector3(200, 0, 0));
            
            yield return StartCoroutine(pannelController.CustomFade(0.85f, 0.3f));

            yield return new WaitForSeconds(1.0f);
        }


        //// 패널 움직임 종료 시 괴물 움직임 멈춤
        //StopCoroutine(monsterControl);

        // 이동 불가 상태 해제
        characterMover.moveType = CharacterMover.MoveType.COMMANDMOVE;

        yield break;
    }
    
    IEnumerator MonsterControl()
    {
        Debug.Log("몬스터 출발");
        yield return new WaitForSeconds(0.3f);
        objMonster.GetComponent<Rigidbody2D>().AddForce(new Vector3(200, 0, 0));
        //this.AudioSource.Play();
        yield break;
    }

    // 1초 대기후 페이드 인~아웃 반복
    IEnumerator PannelControl()
    {
        
        yield break;
    }

    // 괴물 움직임 + 페이드 효과 루틴
    

}
